using Microsoft.AspNetCore.Mvc;
using SellBook.Models;
using SellBook.DataAccess;
using SellBook.DataAccess.Repository.IRepository;

namespace WebsiteSellBook.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryRepository _category;

		public CategoryController(ICategoryRepository category)
		{
			_category = category;
		}
		public IActionResult Index()
		{
			IEnumerable<Category> objCategoryList = _category.GetAll();
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
				_category.Add(category);
				_category.Save();
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
			var exitId = _category.Get(item => item.Category_ID == id);
			return View(exitId);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditCategory(Category category)
		{
			if (ModelState.IsValid)
			{
				_category.Update(category);
				_category.Save();
				TempData["Success"] = "Update category successful !!!";
				TempData["Title"] = "Update category";
				return RedirectToAction("Index");
			}

			return View(category);

		}





		public IActionResult DeleteCategory(int id)
		{
			var exitId = _category.Get(item => item.Category_ID == id);
			if (exitId != null)
			{
				_category.Remove(exitId);
				_category.Save();
				TempData["Title"] = "Remove category";
				TempData["Success"] = "Remove category successful !!!";
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");

		}



	}
}
