using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellBook.DataAccess;
using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using SellBook.Models.ViewModels;
using System.Security.Claims;

namespace WebsiteSellBook.Areas.Customer.Controllers
{
	[Area(nameof(Customer))]
	[Authorize]
	public class ShoppingCartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ShoppingCartVM ShoppingCartVM { get; set; }

		public ShoppingCartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
			ShoppingCartVM = new()
			{
				shoppingCartList = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUser.Id == userId, includeProperties: "Product"),

			};

			foreach (var cart in ShoppingCartVM.shoppingCartList)
			{
				cart.Price = GetPriceOnQuantity(cart);
				ShoppingCartVM.OrderTotal += (cart.Count * cart.Price);
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
			var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
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
			var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
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
			var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
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

		public IActionResult Summary(int id)
		{
			return View();
		}
	}
}
