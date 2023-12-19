using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICategoryRepository Category { get; }
		IProductRepository Product { get; }

		ICompanyRepository Company { get; }
		IShopingCartRepository ShoppingCart { get; }
		IApplicationUserRepository ApplicationUser { get; }

		IOrderDetail OrderDetail { get; }

		IOrderHeader OrderHeader { get; }
		void Save();
	}
}
