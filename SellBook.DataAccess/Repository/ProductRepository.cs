using SellBook.DataAccess.Repository.IRepository;
using SellBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.DataAccess.Repository
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly ApplicationDbContext _db;
		public ProductRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public void Update(Product product)
		{
			//var exitProduct = _db.Products.FirstOrDefault(item => item.Product_Id == product.Product_Id);
			//if (exitProduct != null)
			//{
			//	exitProduct.ISBN = product.ISBN;
			//	exitProduct.Author = product.Author;
			//	exitProduct.ProductDescription = product.ProductDescription;
			//	exitProduct.CategoryId = product.CategoryId;
			//	exitProduct.ListPrice = product.ListPrice;
			//	exitProduct.Price = product.Price;
			//	exitProduct.Price50 = product.Price50;
			//	exitProduct.Price100 = product.Price100;
			//	if (exitProduct.ProductImageUrl != null)
			//	{
			//		exitProduct.ProductImageUrl = product.ProductImageUrl; 
			//	}
			//}
			_db.Update(product);

		}
	}
}
