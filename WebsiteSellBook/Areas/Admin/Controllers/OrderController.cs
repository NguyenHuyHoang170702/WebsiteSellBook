using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;
using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using SellBook.Utility;
using System.Diagnostics;

namespace WebsiteSellBook.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class OrderController : Controller
	{

		private readonly IUnitOfWork _unitOfWork;

		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			return View();
		}

		#region API Calls

		[HttpGet]
		public IActionResult getAll(string status)
		{
			IEnumerable<OrderHeader> orderHeader = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();

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
