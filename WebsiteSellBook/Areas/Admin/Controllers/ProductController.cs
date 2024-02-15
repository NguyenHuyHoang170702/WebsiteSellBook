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
				productVM.Product = _unitOfWork.Product.Get(item => item.Product_Id == id, includeProperties: "ProductImages");
				return View(productVM);
			}

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateAndUpdateProduct(ProductVM productVM, List<IFormFile?> files)
		{

			if (ModelState.IsValid)
			{

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
				string wwwRootPath = _webHostEnvironment.WebRootPath;

				if (files != null)
				{
					foreach (IFormFile file in files)
					{
						// random file name
						string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
						// create file have images, name file is random name + product id
						string productPath = @"images\products\product-" + productVM.Product.Product_Id;
						// final path = path to root file + productpath  
						string finalPath = Path.Combine(wwwRootPath, productPath);

						if (!Directory.Exists(finalPath))
						{
							Directory.CreateDirectory(finalPath);
						}
						using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
						{
							file.CopyTo(fileStream);
						}
						ProductImage productImage = new ProductImage()
						{
							ImgUrl = @"\" + productPath + @"\" + fileName,
							ProductId = productVM.Product.Product_Id,
						};
						// if product images is null, not receive exception
						if (productVM.Product.ProductImages == null)
						{
							productVM.Product.ProductImages = new List<ProductImage>();
						}
						productVM.Product.ProductImages.Add(productImage);

					}
					_unitOfWork.Product.Update(productVM.Product);
					_unitOfWork.Save();

				}
				TempData["Success"] = "Create/Update product successful !!!";
				TempData["Title"] = "Create/Update product";

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

		public IActionResult DeleteImage(int ImageId)
		{
			var imagesToBeDelete = _unitOfWork.ProductImage.Get(item => item.Id == ImageId);
			var productId = imagesToBeDelete.ProductId;
			string wwwRootPath = _webHostEnvironment.WebRootPath;
			if (imagesToBeDelete != null)
			{
				if (!string.IsNullOrEmpty(imagesToBeDelete.ImgUrl))
				{
					var oldImagePath = Path.Combine(wwwRootPath, imagesToBeDelete.ImgUrl.TrimStart('\\'));
					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}
				}

				_unitOfWork.ProductImage.Remove(imagesToBeDelete);
				_unitOfWork.Save();
				TempData["Success"] = "Delete Image successful !!!";
			}
			return RedirectToAction(nameof(CreateAndUpdateProduct), new { id = productId });
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
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				// create file have images, name file is random name + product id
				string productPath = @"images\products\product-" + exitId.Product_Id;
				// final path = path to root file + productpath  
				string finalPath = Path.Combine(wwwRootPath, productPath);

				if (Directory.Exists(finalPath))
				{
					string[] filePaths = Directory.GetFiles(finalPath);
					foreach (string filePath in filePaths)
					{
						System.IO.File.Delete(filePath);
					}
					Directory.Delete(finalPath);
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
