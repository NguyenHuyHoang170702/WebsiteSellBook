﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SellBook.Models;
using SellBook.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.DataAccess.DbInitializer
{
	public class DbInitializer : IDbInitializer
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _db;

		public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_db = db;
		}
		public async void Initialize()
		{
			// migartion if they're not	applied
			try
			{
				if (_db.Database.GetPendingMigrations().Count() > 0)
				{
					_db.Database.Migrate();
				}
			}
			catch (Exception ex)
			{

			}
			// create roles if they're not created
			#region add sample data to role identity
			if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();


				// if role are not created, then we will create admin user as well

				_userManager.CreateAsync(new ApplicationUser
				{
					UserName = "admin@gmail.com",
					Email = "admin@gmail.com",
					Name = "Ma Dao To Su",
					PhoneNumber = "1234567890",
					StressAddress = "test 123 ave",
					State = "IL",
					PostalCode = "12345",
					City = "BinhDuong",


				}, "011223445aA@").GetAwaiter().GetResult();

				ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(item => item.Email == "admin@gmail.com");
				_userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
			}
			#endregion
			return;
		}
	}
}
