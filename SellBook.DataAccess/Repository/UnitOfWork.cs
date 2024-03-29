﻿using Microsoft.AspNetCore.Mvc;
using SellBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public ICategoryRepository Category { get; private set; }

		public IProductRepository Product { get; private set; }

		public ICompanyRepository Company { get; private set; }

		public IShopingCartRepository ShoppingCart { get; private set; }

		public IApplicationUserRepository ApplicationUser { get; private set; }

		public IOrderDetail OrderDetail { get; private set; }

		public IOrderHeader OrderHeader { get; private set; }

		public IProductImageRepository ProductImage { get; private set; }

		private ApplicationDbContext _db;
		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			Category = new CategoryRepository(_db);
			Product = new ProductRepository(_db);
			Company = new CompanyRepository(_db);
			ShoppingCart = new ShoppingCartRepository(_db);
			ApplicationUser = new ApplicationUserRepository(_db);
			OrderDetail = new OrderDetailRepository(_db);
			OrderHeader = new OrderHeaderRepository(_db);
			ProductImage = new ProductImageRepository(_db);
		}
		public void Save()
		{
			_db.SaveChanges();
		}

	}
}
