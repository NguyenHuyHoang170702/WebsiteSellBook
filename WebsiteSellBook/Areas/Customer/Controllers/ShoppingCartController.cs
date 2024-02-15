using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using SellBook.DataAccess;
using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using SellBook.Models.ViewModels;
using SellBook.Utility;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace WebsiteSellBook.Areas.Customer.Controllers
{
	[Area(nameof(Customer))]
	[Authorize]
	public class ShoppingCartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IEmailSender _emailSender;

		[BindProperty]
		public ShoppingCartVM ShoppingCartVM { get; set; }

		public ShoppingCartController(IUnitOfWork unitOfWork, IEmailSender emailSender)
		{
			_unitOfWork = unitOfWork;
			_emailSender = emailSender;
		}

		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			ShoppingCartVM = new()
			{
				shoppingCartList = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == userId, includeProperties: "Product"),
				orderHeader = new OrderHeader()
			};

			IEnumerable<ProductImage> productImages = _unitOfWork.ProductImage.GetAll();

			foreach (var cart in ShoppingCartVM.shoppingCartList)
			{
				cart.Product.ProductImages = productImages.Where(item => item.ProductId == cart.Product.Product_Id).ToList();
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

		public IActionResult Plus(int Productid)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			var exitProduct = _unitOfWork.Product.Get(item => item.Product_Id == Productid);
			if (exitProduct != null)
			{
				var updateCart = _unitOfWork.ShoppingCart.Get(item => item.ApplicationUserId == userId && item.ProductId == exitProduct.Product_Id);
				if (updateCart != null)
				{
					updateCart.Count += 1;
					_unitOfWork.ShoppingCart.Update(updateCart);
					_unitOfWork.Save();
				}
			}
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Minus(int Productid)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			var exitProduct = _unitOfWork.Product.Get(item => item.Product_Id == Productid);
			if (exitProduct != null)
			{
				var updateCart = _unitOfWork.ShoppingCart.Get(item => item.ApplicationUserId == userId && item.ProductId == exitProduct.Product_Id);
				if (updateCart.Count > 1)
				{
					updateCart.Count -= 1;
					_unitOfWork.ShoppingCart.Update(updateCart);
				}
				else if (updateCart.Count <= 1)
				{
					_unitOfWork.ShoppingCart.Remove(updateCart);
					HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == userId).Count() - 1);
				}
				_unitOfWork.Save();
			}
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int Productid)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			var exitProduct = _unitOfWork.Product.Get(item => item.Product_Id == Productid);
			if (exitProduct != null)
			{
				var updateCart = _unitOfWork.ShoppingCart.Get(item => item.ApplicationUser.Id == userId && item.Product.Product_Id == exitProduct.Product_Id);
				if (updateCart != null)
				{
					_unitOfWork.ShoppingCart.Remove(updateCart);
					HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == userId).Count() - 1);
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
				var domain = "https://localhost:44339/";
				var options = new Stripe.Checkout.SessionCreateOptions
				{
					SuccessUrl = domain + $"customer/shoppingcart/OrderComfirmation?id={ShoppingCartVM.orderHeader.Id}",
					CancelUrl = domain + "customer/shoppingcart/index",
					LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
					Mode = "payment",
				};

				foreach (var item in ShoppingCartVM.shoppingCartList)
				{
					var sessionLineItem = new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmount = (long)(item.Price * 100),
							Currency = "usd",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = item.Product.Title
							}
						},
						Quantity = item.Count,
					};
					options.LineItems.Add(sessionLineItem);
				}
				var service = new SessionService();
				Session session = service.Create(options);

				_unitOfWork.OrderHeader.UpdateStripePaymentId(ShoppingCartVM.orderHeader.Id, session.Id, session.PaymentIntentId);
				_unitOfWork.Save();

				Response.Headers.Add("location", session.Url);
				return new StatusCodeResult(303);
			}
			return RedirectToAction(nameof(OrderComfirmation), new { id = ShoppingCartVM.orderHeader.Id });
		}


		public IActionResult OrderComfirmation(int id)
		{
			OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(item => item.Id == id, includeProperties: "ApplicationUser");
			if (orderHeader.PaymentStatus != SD.PaymentStatusDelayedPayment)
			{
				var service = new SessionService();
				Session session = service.Get(orderHeader.SessionId);
				if (session.PaymentStatus.ToLower() == "paid")
				{
					_unitOfWork.OrderHeader.UpdateStripePaymentId(orderHeader.Id, session.Id, session.PaymentIntentId);
					_unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
					_unitOfWork.Save();
				}
				HttpContext.Session.Clear();
			}
			_emailSender.SendEmailAsync(orderHeader.ApplicationUser.Email, "New Order - Sell Book", $"<p>Your order is success, order id - {orderHeader.Id}</p>");
			List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == orderHeader.ApplicationUserId).ToList();
			_unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
			_unitOfWork.Save();

			return View(id);
		}
	}
}
