using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.DataAccess.Repository
{
	public class ShoppingCartRepository : Repository<ShoppingCart>, IShopingCartRepository
	{
		private readonly ApplicationDbContext _db;
		public ShoppingCartRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(ShoppingCart shoppingCart)
		{
			_db.Update(shoppingCart);
		}
	}
}
