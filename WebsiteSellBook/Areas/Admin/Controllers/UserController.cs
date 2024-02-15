using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SellBook.DataAccess;
using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using SellBook.Models.ViewModels;
using SellBook.Utility;
using SQLitePCL;

namespace WebsiteSellBook.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class UserController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		[BindProperty]
		public UserVM userVM { get; set; }
		public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public IActionResult Index()
		{
			return View();
		}


		public IActionResult Permissions(string userId)
		{

			userVM = new UserVM()
			{
				applicationUser = _unitOfWork.ApplicationUser.Get(item => item.Id == userId, includeProperties: "Company"),
				RoleList = _roleManager.Roles.Select(item => new SelectListItem
				{
					Text = item.Name,
					Value = item.Name,
				}),
				CompanyList = _unitOfWork.Company.GetAll().Select(item => new SelectListItem
				{
					Text = item.CompanyName,
					Value = item.CpmpanyId.ToString()
				}),
			};
			userVM.applicationUser.Role = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.Get(item => item.Id == userId))
				.GetAwaiter().GetResult().FirstOrDefault();

			return View(userVM);
		}

		[HttpPost]
		public IActionResult Permissions()
		{

			string oldRole = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.Get(item => item.Id == userVM.applicationUser.Id))
				.GetAwaiter().GetResult().FirstOrDefault();
			if (!(userVM.applicationUser.Role == oldRole))
			{
				// a role was update
				ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(item => item.Id == userVM.applicationUser.Id);
				if (userVM.applicationUser.Role == SD.Role_Company)
				{
					applicationUser.CompanyId = userVM.applicationUser.CompanyId;
				}
				if (oldRole == SD.Role_Company)
				{
					applicationUser.CompanyId = null;
				}
				_unitOfWork.ApplicationUser.Update(applicationUser);
				_unitOfWork.Save();
				_userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
				_userManager.AddToRoleAsync(applicationUser, userVM.applicationUser.Role).GetAwaiter().GetResult();
			}
			else
			{
				ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(item => item.Id == userVM.applicationUser.Id);
				if (userVM.applicationUser.Role == SD.Role_Company)
				{
					applicationUser.CompanyId = userVM.applicationUser.CompanyId;
					_unitOfWork.Save();
				}
			}

			return RedirectToAction(nameof(Index));
		}



		#region API Calls
		[HttpGet]
		public IActionResult GetAll()
		{
			var getData = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company").ToList();


			foreach (var user in getData)
			{
				user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
			}



			return Json(new { data = getData });
		}


		[HttpPost]
		public IActionResult LockUnlock([FromBody] string id)
		{

			var objFromDb = _unitOfWork.ApplicationUser.Get(item => item.Id == id);
			if (objFromDb == null)
			{
				return Json(new
				{
					success = false,
					message = "Error While Locking/Unlocking",
					title = "Lock/Unlock",
					icon = "error",
				});
			}

			if (objFromDb != null && objFromDb.LockoutEnd > DateTime.Now)
			{
				// user is current locked, and we need to unlock them
				objFromDb.LockoutEnd = DateTime.Now;
			}
			else
			{
				// if you lock user, they can't login 1000 years
				objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
			}
			_unitOfWork.ApplicationUser.Update(objFromDb);
			_unitOfWork.Save();
			return Json(new
			{
				success = true,
				message = "Locking/Unlocking user success",
				title = "Lock/Unlock",
				icon = "success",
			});
		}
		#endregion
	}
}
