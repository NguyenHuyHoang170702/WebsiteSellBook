using Microsoft.AspNetCore.Mvc;
using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using SellBook.Models.ViewModels;
using SQLitePCL;

namespace WebsiteSellBook.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CompanyController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public CompanyController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult CreateAndUpdateCompany(int? id)
		{
			try
			{
				if (id == null || id == 0)
				{
					ViewBag.Title = "Create Company";
					return View();
				}
				else
				{
					var exitCompany = _unitOfWork.Company.Get(item => item.CpmpanyId == id);
					ViewBag.Title = "Edit Company";
					return View(exitCompany);
				}
			}
			catch (Exception ex)
			{
				return View(ex.Message);
			}
		}

		[HttpPost]
		public IActionResult CreateAndUpdateCompany(Company? company)
		{
			if (ModelState.IsValid)
			{
				if (company.CpmpanyId == 0)
				{
					_unitOfWork.Company.Add(company);
					_unitOfWork.Save();
					TempData["Success"] = "Create new Company successful !!!";
					TempData["Title"] = "Create Company";
				}
				else
				{
					_unitOfWork.Company.Update(company);
					_unitOfWork.Save();
					TempData["Success"] = "Update Company successful !!!";
					TempData["Title"] = "Update Company";
				}
				return RedirectToAction("Index");
			}
			else
			{
				return View();
			}
		}



		#region API Calls
		[HttpGet]
		public IActionResult GetAll()
		{
			var getData = _unitOfWork.Company.GetAll().ToList();
			return Json(new { data = getData });
		}
		#endregion
	}
}
