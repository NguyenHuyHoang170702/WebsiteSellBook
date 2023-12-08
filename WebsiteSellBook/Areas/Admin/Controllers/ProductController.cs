using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using SellBook.Models.ViewModels;
using SellBook.Utility;

namespace WebsiteSellBook.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
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
					if (!string.IsNullOrEmpty(productVM.Product.ProductImageUrl))
					{
						var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ProductImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}
					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					productVM.Product.ProductImageUrl = @"\images\products\" + fileName;
				}

				if (productVM.Product.Product_Id == 0)
				{
					_unitOfWork.Product.Add(productVM.Product);
					TempData["Success"] = "Create new product successful !!!";
					TempData["Title"] = "Create product";
				}
				else
				{
					_unitOfWork.Product.Update(productVM.Product);
					TempData["Success"] = "Update product successful !!!";
					TempData["Title"] = "Update product";
				}
				_unitOfWork.Save();

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

		#region API Calls
		[HttpGet]
		public IActionResult GetAllProduct()
		{
			List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
			return Json(new
			{
				data = products
			});
		}

		[HttpDelete]
		public IActionResult DeleteProduct(int id)
		{
			var exitId = _unitOfWork.Product.Get(item => item.Product_Id == id);
			if (exitId != null)
			{
				var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, exitId.ProductImageUrl.TrimStart('\\'));
				if (System.IO.File.Exists(oldImagePath))
				{
					System.IO.File.Delete(oldImagePath);
				}
				_unitOfWork.Product.Remove(exitId);
				_unitOfWork.Save();

				List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
				return Json(new
				{
					data = products,
					message = "Delete product success",
					title = "Deleted",
					icon = "success",
				});
			}
			return Json(new
			{
				success = false,
				message = "Error while deleting",
			});
		}
		#endregion

	}
}
