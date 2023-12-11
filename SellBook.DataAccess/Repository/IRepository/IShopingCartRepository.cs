using SellBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.DataAccess.Repository.IRepository
{
	public interface IShopingCartRepository : IRepository<ShoppingCart>
	{
		public void Update(ShoppingCart shoppingCart);
	}
}
