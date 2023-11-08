using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;

namespace WebsiteSellBook.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ProductController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			List<Product> lstProduct = _unitOfWork.Product.GetAll().ToList();

			if (lstProduct != null)
			{
				return View(lstProduct);
			}
			return View();
		}

		[HttpGet]
		public IActionResult CreateProduct()
		{
			IEnumerable<SelectListItem> lstCategory = _unitOfWork.Category.GetAll().Select(item => new SelectListItem
			{
				Text = item.Category_Name,
				Value = item.Category_ID.ToString(),
			});

			ViewBag.CategoryList = lstCategory;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateProduct(Product product)
		{
			product.ProductImageUrl = "test";

			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Add(product);
				_unitOfWork.Save();
				TempData["Success"] = "Create new product successful !!!";
				TempData["Title"] = "Create product";
				return RedirectToAction("Index");
			}
			return View();
		}


		public IActionResult RemoveProduct(int id)
		{
			var exitId = _unitOfWork.Product.Get(item => item.Product_Id == id);
			if (exitId != null)
			{
				_unitOfWork.Product.Remove(exitId);
				_unitOfWork.Save();
				TempData["Success"] = "Remove product successful !!!";
				TempData["Title"] = "Remove product";
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpGet]
		public IActionResult EditProduct(int id)
		{
			var exitId = _unitOfWork.Product.Get(item => item.Product_Id == id);
			if (exitId != null)
			{
				return View(exitId);
			}
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditProduct(Product product)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Update(product);
				_unitOfWork.Save();
				TempData["Success"] = "Edit product successful !!!";
				TempData["Title"] = "Edit product";
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
