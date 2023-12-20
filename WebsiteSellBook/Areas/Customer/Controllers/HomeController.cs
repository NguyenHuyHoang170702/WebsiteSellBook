using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SellBook.Models;
using SellBook.DataAccess.Repository.IRepository;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebsiteSellBook.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			List<Product> lstProduct = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
			return View(lstProduct);
		}

		public IActionResult Details(int Productid)
		{
			ShoppingCart shoppingCart = new()
			{
				Product = _unitOfWork.Product.Get(item => item.Product_Id == Productid, includeProperties: "Category"),
				Count = 1,
				ProductId = Productid,
			};


			return View(shoppingCart);
		}


		[HttpPost]
		[Authorize]
		public IActionResult Details(ShoppingCart shoppingCart)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;


			ShoppingCart exitShoppingCart = _unitOfWork.ShoppingCart.Get(item => item.ApplicationUserId == userId && item.ProductId == shoppingCart.ProductId);


			if (exitShoppingCart != null)
			{
				exitShoppingCart.Count += shoppingCart.Count;
				_unitOfWork.ShoppingCart.Update(exitShoppingCart);
				TempData["Success"] = "Cart update successful !!!";
				_unitOfWork.Save();
			}
			else
			{
				shoppingCart.ApplicationUserId = userId;
				_unitOfWork.ShoppingCart.Add(shoppingCart);
				TempData["Success"] = "Cart add successful !!!";
				_unitOfWork.Save();

			}

			return RedirectToAction(nameof(Index));
		}



		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}