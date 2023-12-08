using Microsoft.AspNetCore.Mvc;
using SellBook.DataAccess.Repository.IRepository;

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
