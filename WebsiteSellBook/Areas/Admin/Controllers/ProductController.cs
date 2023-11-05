using Microsoft.AspNetCore.Mvc;
using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;

namespace WebsiteSellBook.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ProductController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			IEnumerable<Product> lstProduct = _unitOfWork.Product.GetAll();
			if (lstProduct != null)
			{
				return View(lstProduct);
			}
			return View();
		}
	}
}
