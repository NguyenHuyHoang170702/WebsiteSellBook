using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.DataAccess.Repository
{
	public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeader
	{
		private ApplicationDbContext _db;
		public OrderHeaderRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(OrderHeader orderHeader)
		{
			_db.Update(orderHeader);
		}

		public void UpdateStatus(int id, string Orderstatus, string? paymentStatus = null)
		{
			var orderFromDB = _db.OrderHeaders.FirstOrDefault(item => item.Id == id);
			if (orderFromDB != null)
			{
				orderFromDB.OrderStatus = Orderstatus;
				if (!string.IsNullOrEmpty(paymentStatus))
				{
					orderFromDB.PaymentStatus = paymentStatus;
				}
			}

		}

		public void UpdateStripePaymentId(int id, string sessionId, string? paymentIntentId)
		{
			var orderFromDB = _db.OrderHeaders.FirstOrDefault(item => item.Id == id);
			if (!string.IsNullOrEmpty(sessionId))
			{
				orderFromDB.SessionId = sessionId;
			}

			if (!string.IsNullOrEmpty(paymentIntentId))
			{
				orderFromDB.PaymentIntentId = paymentIntentId;
				orderFromDB.PaymentDate = DateTime.Now;
			}
		}
	}
}
