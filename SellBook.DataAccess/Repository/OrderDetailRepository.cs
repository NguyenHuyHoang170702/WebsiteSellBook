using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.DataAccess.Repository
{
	public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetail
	{
		private ApplicationDbContext _db;
		public OrderDetailRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(OrderDetail orderDetail)
		{
			_db.Update(orderDetail);
		}
	}
}
