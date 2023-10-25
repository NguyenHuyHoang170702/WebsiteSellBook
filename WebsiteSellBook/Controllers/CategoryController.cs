using Microsoft.AspNetCore.Mvc;
using SellBook.Models;
using SellBook.DataAccess;

namespace WebsiteSellBook.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db = new ApplicationDbContext();

		public CategoryController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			IEnumerable<Category> objCategoryList = _db.Categories;
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
				_db.Categories.Add(category);
				_db.SaveChanges();
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
			var exitId = _db.Categories.Single(x => x.Category_ID == id);
			return View(exitId);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditCategory(Category category)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Update(category);
				_db.SaveChanges();
				TempData["Success"] = "Update category successful !!!";
				TempData["Title"] = "Update category";
				return RedirectToAction("Index");
			}

			return View(category);

		}





		public IActionResult DeleteCategory(int id)
		{
			var exitId = _db.Categories.Find(id);
			if (exitId != null)
			{
				_db.Categories.Remove(exitId);
				_db.SaveChanges();
				TempData["Title"] = "Remove category";
				TempData["Success"] = "Remove category successful !!!";
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");

		}



	}
}
