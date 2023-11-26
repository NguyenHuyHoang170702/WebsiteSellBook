using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SellBook.Models;
using SellBook.DataAccess.Repository.IRepository;

namespace WebsiteSellBook.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			List<Product> lstProduct = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
			return View(lstProduct);
		}

		public IActionResult Details(int? id)
		{
			Product exitProduct = _unitOfWork.Product.Get(item => item.Product_Id == id, includeProperties: "Category");
			return View(exitProduct);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}