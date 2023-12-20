using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellBook.DataAccess;
using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using SellBook.Models.ViewModels;
using SellBook.Utility;
using System.Security.Claims;

namespace WebsiteSellBook.Areas.Customer.Controllers
{
	[Area(nameof(Customer))]
	[Authorize]
	public class ShoppingCartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		[BindProperty]
		public ShoppingCartVM ShoppingCartVM { get; set; }

		public ShoppingCartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			ShoppingCartVM = new()
			{
				shoppingCartList = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUser.Id == userId, includeProperties: "Product"),
				orderHeader = new OrderHeader()
			};

			foreach (var cart in ShoppingCartVM.shoppingCartList)
			{
				cart.Price = GetPriceOnQuantity(cart);
				ShoppingCartVM.orderHeader.OrderTotal += (cart.Count * cart.Price);
			}

			return View(ShoppingCartVM);
		}

		private double GetPriceOnQuantity(ShoppingCart shoppingCart)
		{
			double quantity = shoppingCart.Count;

			if (quantity <= 50)
			{
				return shoppingCart.Product.Price;
			}
			else if (quantity <= 100)
			{
				return shoppingCart.Product.Price50;
			}
			else
			{
				return shoppingCart.Product.Price100;
			}

		}

		public IActionResult Plus(int id)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			var exitProduct = _unitOfWork.Product.Get(item => item.Product_Id == id);
			if (exitProduct != null)
			{
				var updateCart = _unitOfWork.ShoppingCart.Get(item => item.ApplicationUser.Id == userId && item.Product.Product_Id == exitProduct.Product_Id);
				if (updateCart != null)
				{
					updateCart.Count += 1;
					_unitOfWork.ShoppingCart.Update(updateCart);
					_unitOfWork.Save();
				}
			}
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Minus(int id)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			var exitProduct = _unitOfWork.Product.Get(item => item.Product_Id == id);
			if (exitProduct != null)
			{
				var updateCart = _unitOfWork.ShoppingCart.Get(item => item.ApplicationUser.Id == userId && item.Product.Product_Id == exitProduct.Product_Id);
				if (updateCart.Count > 1)
				{
					updateCart.Count -= 1;
					_unitOfWork.ShoppingCart.Update(updateCart);
				}
				else if (updateCart.Count <= 1)
				{
					_unitOfWork.ShoppingCart.Remove(updateCart);
				}
				_unitOfWork.Save();
			}
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int id)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			var exitProduct = _unitOfWork.Product.Get(item => item.Product_Id == id);
			if (exitProduct != null)
			{
				var updateCart = _unitOfWork.ShoppingCart.Get(item => item.ApplicationUser.Id == userId && item.Product.Product_Id == exitProduct.Product_Id);
				if (updateCart != null)
				{
					_unitOfWork.ShoppingCart.Remove(updateCart);
					_unitOfWork.Save();
				}
			}
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult Summary()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			ShoppingCartVM = new()
			{
				shoppingCartList = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUser.Id == userId, includeProperties: "Product"),
				orderHeader = new OrderHeader()
			};

			ShoppingCartVM.orderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(item => item.Id == userId);
			ShoppingCartVM.orderHeader.ApplicationUserId = ShoppingCartVM.orderHeader.ApplicationUser.Id;
			ShoppingCartVM.orderHeader.Name = ShoppingCartVM.orderHeader.ApplicationUser.Name;
			ShoppingCartVM.orderHeader.PhoneNumber = ShoppingCartVM.orderHeader.ApplicationUser.PhoneNumber;
			ShoppingCartVM.orderHeader.StressAddress = ShoppingCartVM.orderHeader.ApplicationUser.StressAddress;
			ShoppingCartVM.orderHeader.City = ShoppingCartVM.orderHeader.ApplicationUser.City;
			ShoppingCartVM.orderHeader.State = ShoppingCartVM.orderHeader.ApplicationUser.State;
			ShoppingCartVM.orderHeader.PostalCode = ShoppingCartVM.orderHeader.ApplicationUser.PostalCode;

			foreach (var cart in ShoppingCartVM.shoppingCartList)
			{
				cart.Price = GetPriceOnQuantity(cart);
				ShoppingCartVM.orderHeader.OrderTotal += (cart.Count * cart.Price);
			}
			return View(ShoppingCartVM);
		}

		[HttpPost]
		[ActionName("Summary")]
		public IActionResult SummaryPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			ShoppingCartVM.shoppingCartList = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == userId, includeProperties: "Product");
			ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(item => item.Id == userId);

			foreach (var cart in ShoppingCartVM.shoppingCartList)
			{
				cart.Price = GetPriceOnQuantity(cart);
				ShoppingCartVM.orderHeader.OrderTotal += (cart.Count * cart.Price);
			}

			if (applicationUser.CompanyId.GetValueOrDefault() == 0)
			{
				ShoppingCartVM.orderHeader.OrderStatus = SD.StatusPending;
				ShoppingCartVM.orderHeader.PaymentStatus = SD.PaymentStatusPending;
			}
			else
			{
				ShoppingCartVM.orderHeader.OrderStatus = SD.StatusApproved;
				ShoppingCartVM.orderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
			}

			_unitOfWork.OrderHeader.Add(ShoppingCartVM.orderHeader);
			_unitOfWork.Save();

			foreach (var cart in ShoppingCartVM.shoppingCartList)
			{
				OrderDetail orderDetail = new OrderDetail()
				{
					OrderHeaderId = ShoppingCartVM.orderHeader.Id,
					ProductId = cart.ProductId,
					Count = cart.Count,
					Price = cart.Price
				};
				_unitOfWork.OrderDetail.Add(orderDetail);
				_unitOfWork.Save();
			}

			if (applicationUser.CompanyId.GetValueOrDefault() == 0)
			{
				//Stripe logic
			}
			return RedirectToAction(nameof(OrderComfirmation), new { id = ShoppingCartVM.orderHeader.Id });
		}


		public IActionResult OrderComfirmation(int id)
		{
			return View(id);
		}
	}
}
