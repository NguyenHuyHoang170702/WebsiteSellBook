﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SellBook.DataAccess.Repository.IRepository;
namespace SellBook.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		// handle function implement to IRepository
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
		public Repository(ApplicationDbContext db)
		{
			_db = db;
			this.dbSet = _db.Set<T>();
			_db.Products.Include(item => item.Category).Include(item => item.CategoryId);
		}
		public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
		{
			IQueryable<T> query;
			if (tracked == true)
			{
				query = dbSet;
			}
			else
			{
				query = dbSet.AsNoTracking();

			}

			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return query.Where(filter).FirstOrDefault();
		}

		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return query.ToList();
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void Remove(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}
	}
}
