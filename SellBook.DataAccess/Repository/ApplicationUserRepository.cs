﻿using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.DataAccess.Repository
{
	public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
	{
		private readonly ApplicationDbContext _db;
		public ApplicationUserRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(ApplicationUser applicationUser)
		{
			_db.Update(applicationUser);
		}
	}
}
