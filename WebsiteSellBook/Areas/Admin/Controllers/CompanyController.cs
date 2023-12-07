using Microsoft.AspNetCore.Mvc;

namespace WebsiteSellBook.Areas.Admin.Controllers
{
	public class CompanyController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
