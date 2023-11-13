using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using SellBook.Models.ViewModels;

namespace WebsiteSellBook.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
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
		public IActionResult CreateAndUpdateProduct(int? id)
		{
			ProductVM productVM = new()
			{
				CategoryList = _unitOfWork.Category.GetAll().Select(item => new SelectListItem
				{
					Text = item.Category_Name,
					Value = item.Category_ID.ToString(),
				}),
				Product = new Product()
			};
			if (id == null || id == 0)
			{
				ViewBag.Title = "Create Product";
				return View(productVM);

			}
			else
			{
				ViewBag.Title = "Update Product";
				productVM.Product = _unitOfWork.Product.Get(item => item.Product_Id == id);
				return View(productVM);
			}

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateAndUpdateProduct(ProductVM productVM, IFormFile? file)
		{

			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string productPath = Path.Combine(wwwRootPath, @"images\products");

					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					productVM.Product.ProductImageUrl = @"\images\products\" + fileName;
				}
				_unitOfWork.Product.Add(productVM.Product);
				_unitOfWork.Save();
				TempData["Success"] = "Create new product successful !!!";
				TempData["Title"] = "Create product";
				return RedirectToAction("Index");
			}
			else
			{
				productVM.CategoryList = _unitOfWork.Category.GetAll().Select(item => new SelectListItem
				{
					Text = item.Category_Name,
					Value = item.Category_ID.ToString(),
				});
				return View(productVM);
			}

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
	}
}
