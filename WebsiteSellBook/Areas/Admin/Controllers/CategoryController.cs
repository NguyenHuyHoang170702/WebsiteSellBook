using Microsoft.AspNetCore.Mvc;
using SellBook.Models;
using SellBook.DataAccess;
using SellBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using SellBook.Utility;

namespace WebsiteSellBook.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class CategoryController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
			//int page = 1;
			//var itemOnePage = 2;
			//var totalPage = (int)Math.Ceiling(objCategoryList.Count() / (double)itemOnePage);
			//var lstItem = objCategoryList.Reverse().Skip((page - 1) * itemOnePage).Take(itemOnePage).ToList();
			return View(objCategoryList);
		}

		[HttpGet]
		public IActionResult CreateCategory()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateCategory(Category category)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Category.Add(category);
				_unitOfWork.Save();
				TempData["Success"] = "Create new category successful !!!";
				TempData["Title"] = "Create category";
				return RedirectToAction("Index");
			}
			else
			{
				return View(category);
			}

		}

		[HttpGet]
		public IActionResult EditCategory(int? id)
		{
			var exitId = _unitOfWork.Category.Get(item => item.Category_ID == id);
			return View(exitId);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditCategory(Category category)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Category.Update(category);
				_unitOfWork.Save();
				TempData["Success"] = "Update category successful !!!";
				TempData["Title"] = "Update category";
				return RedirectToAction("Index");
			}

			return View(category);

		}

		public IActionResult DeleteCategory(int id)
		{
			var exitId = _unitOfWork.Category.Get(item => item.Category_ID == id);
			if (exitId != null)
			{
				_unitOfWork.Category.Remove(exitId);
				_unitOfWork.Save();
				TempData["Title"] = "Remove category";
				TempData["Success"] = "Remove category successful !!!";
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");

		}



	}
}
