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
		private readonly ApplicationDbContext _db;
		private readonly UserManager<IdentityUser> _userManager;

		[BindProperty]
		public UserVM userVM { get; set; }
		public UserController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}
		public IActionResult Index()
		{
			return View();
		}


		public IActionResult Permissions(string userId)
		{
			string roleId = _db.UserRoles.FirstOrDefault(x => x.UserId == userId).RoleId;
			userVM = new UserVM()
			{
				applicationUser = _db.ApplicationUsers.Include(item => item.Company).FirstOrDefault(item => item.Id == userId),
				RoleList = _db.Roles.Select(item => new SelectListItem
				{
					Text = item.Name,
					Value = item.Name,
				}),
				CompanyList = _db.Companies.Select(item => new SelectListItem
				{
					Text = item.CompanyName,
					Value = item.CpmpanyId.ToString()
				}),
			};
			userVM.applicationUser.Role = _db.Roles.FirstOrDefault(item => item.Id == roleId).Name;

			return View(userVM);
		}

		[HttpPost]
		public IActionResult Permissions()
		{
			string roleId = _db.UserRoles.FirstOrDefault(x => x.UserId == userVM.applicationUser.Id).RoleId;
			string oldRole = _db.Roles.FirstOrDefault(item => item.Id == roleId).Name;
			if (!(userVM.applicationUser.Role == oldRole))
			{
				// a role was update
				ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(item => item.Id == userVM.applicationUser.Id);
				if (userVM.applicationUser.Role == SD.Role_Company)
				{
					applicationUser.CompanyId = userVM.applicationUser.CompanyId;
				}
				if (oldRole == SD.Role_Company)
				{
					applicationUser.CompanyId = null;
				}
				_db.SaveChanges();
				_userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
				_userManager.AddToRoleAsync(applicationUser, userVM.applicationUser.Role).GetAwaiter().GetResult();
			}
			else
			{
				ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(item => item.Id == userVM.applicationUser.Id);
				if (userVM.applicationUser.Role == SD.Role_Company)
				{
					applicationUser.CompanyId = userVM.applicationUser.CompanyId;
					_db.SaveChanges();
				}
			}

			return RedirectToAction(nameof(Index));
		}



		#region API Calls
		[HttpGet]
		public IActionResult GetAll()
		{
			var getData = _db.ApplicationUsers.Include(item => item.Company).ToList();

			var userRoles = _db.UserRoles.ToList();
			var roles = _db.Roles.ToList();

			foreach (var user in getData)
			{
				var roleId = userRoles.FirstOrDefault(item => item.UserId == user.Id).RoleId;
				user.Role = roles.FirstOrDefault(item => item.Id == roleId).Name;

			}



			return Json(new { data = getData });
		}


		[HttpPost]
		public IActionResult LockUnlock([FromBody] string id)
		{

			var objFromDb = _db.ApplicationUsers.FirstOrDefault(item => item.Id == id);
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
			_db.SaveChanges();
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
