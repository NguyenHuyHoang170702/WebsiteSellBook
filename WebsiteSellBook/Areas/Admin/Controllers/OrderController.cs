using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;
using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using SellBook.Models.ViewModels;
using SellBook.Utility;
using Stripe;
using Stripe.Climate;
using System.Diagnostics;
using System.Security.Claims;

namespace WebsiteSellBook.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OrderController : Controller
	{

		private readonly IUnitOfWork _unitOfWork;

		[BindProperty]
		public OrderVM orderVM { get; set; }

		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Details(int orderId)
		{
			orderVM = new()
			{
				orderHeader = _unitOfWork.OrderHeader.Get(item => item.Id == orderId, includeProperties: "ApplicationUser"),
				orderDetails = _unitOfWork.OrderDetail.GetAll(item => item.OrderHeaderId == orderId, includeProperties: "Product"),
			};
			return View(orderVM);
		}

		[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
		public IActionResult UpdateOrderDetails()
		{
			var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(item => item.Id == orderVM.orderHeader.Id);

			orderHeaderFromDb.Name = orderVM.orderHeader.Name;
			orderHeaderFromDb.PhoneNumber = orderVM.orderHeader.PhoneNumber;
			orderHeaderFromDb.StressAddress = orderVM.orderHeader.StressAddress;
			orderHeaderFromDb.City = orderVM.orderHeader.City;
			orderHeaderFromDb.State = orderVM.orderHeader.State;
			orderHeaderFromDb.PostalCode = orderVM.orderHeader.PostalCode;
			if (!String.IsNullOrEmpty(orderVM.orderHeader.Carrier))
			{
				orderHeaderFromDb.Carrier = orderVM.orderHeader.Carrier;
			}
			if (!String.IsNullOrEmpty(orderVM.orderHeader.TrackingNumber))
			{
				orderHeaderFromDb.TrackingNumber = orderVM.orderHeader.TrackingNumber;
			}

			_unitOfWork.OrderHeader.Update(orderHeaderFromDb);
			_unitOfWork.Save();
			TempData["Success"] = "Update Order details successful !!!";
			return RedirectToAction(nameof(Details), new { orderId = orderHeaderFromDb.Id });
		}

		[HttpPost]
		[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
		public IActionResult StartProcessing()
		{
			_unitOfWork.OrderHeader.UpdateStatus(orderVM.orderHeader.Id, SD.StatusInProcess);
			_unitOfWork.Save();
			TempData["Success"] = "Update Order details successful !!!";
			return RedirectToAction(nameof(Details), new { orderId = orderVM.orderHeader.Id });
		}

		[HttpPost]
		[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
		public IActionResult ShipOrder()
		{
			var orderHeader = _unitOfWork.OrderHeader.Get(item => item.Id == orderVM.orderHeader.Id);
			orderHeader.TrackingNumber = orderVM.orderHeader.TrackingNumber;
			orderHeader.Carrier = orderVM.orderHeader.Carrier;
			orderHeader.OrderStatus = SD.StatusShipped;
			orderHeader.ShippingDate = DateTime.Now;
			if (orderHeader.OrderStatus == SD.PaymentStatusDelayedPayment)
			{
				orderHeader.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
			}
			_unitOfWork.OrderHeader.Update(orderHeader);
			_unitOfWork.Save();
			TempData["Success"] = "Order ShippedS successful !!!";
			return RedirectToAction(nameof(Details), new { orderId = orderVM.orderHeader.Id });
		}


		[HttpPost]
		[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
		public IActionResult CancelOrder()
		{
			var orderHeader = _unitOfWork.OrderHeader.Get(item => item.Id == orderVM.orderHeader.Id);
			if (orderHeader.PaymentStatus == SD.StatusApproved)
			{
				var options = new RefundCreateOptions
				{
					Reason = RefundReasons.RequestedByCustomer,
					PaymentIntent = orderHeader.PaymentIntentId
				};

				var service = new RefundService();
				Refund refund = service.Create(options);
				_unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusRefunded);
			}
			else
			{
				_unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusCancelled);
			}

			_unitOfWork.Save();
			TempData["Success"] = "Order Cancel successful !!!";
			return RedirectToAction(nameof(Details), new { orderId = orderVM.orderHeader.Id });
		}

		#region API Calls

		[HttpGet]
		public IActionResult getAll(string status)
		{
			IEnumerable<OrderHeader> orderHeader;

			if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
			{
				orderHeader = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
			}
			else
			{
				var claimsIdentity = (ClaimsIdentity)User.Identity;
				var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
				orderHeader = _unitOfWork.OrderHeader.GetAll(item => item.ApplicationUserId == userId, includeProperties: "ApplicationUser").ToList();
			}

			switch (status)
			{
				case "pending":
					orderHeader = orderHeader.Where(item => item.PaymentStatus == SD.PaymentStatusDelayedPayment); break;
				case "inprocess":
					orderHeader = orderHeader.Where(item => item.OrderStatus == SD.StatusInProcess); break;
				case "completed":
					orderHeader = orderHeader.Where(item => item.OrderStatus == SD.StatusShipped); break;
				case "approved":
					orderHeader = orderHeader.Where(item => item.OrderStatus == SD.StatusApproved); break;
				default:
					break;
			}

			return Json(new { data = orderHeader });
		}

		#endregion
	}
}
