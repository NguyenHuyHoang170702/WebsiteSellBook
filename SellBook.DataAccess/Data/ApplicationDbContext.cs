using Microsoft.EntityFrameworkCore;
using SellBook.Models;

namespace SellBook.DataAccess
{
	public class ApplicationDbContext : DbContext
	{
		//Custom Db context to choose database and connection string 
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public ApplicationDbContext()
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			#region Add sample data for Product
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					Product_Id = 1,
					Title = "TEST1",
					ProductDescription = "1TTTTT",
					Author = "LOL",
					ISBN = "QWE",
					ListPrice = 10,
					Price = 10,
					Price50 = 8,
					Price100 = 6
				},
				new Product
				{
					Product_Id = 2,
					Title = "TEST2",
					ProductDescription = "2TTTTT",
					Author = "LOL",
					ISBN = "QWE",
					ListPrice = 10,
					Price = 10,
					Price50 = 8,
					Price100 = 6
				},
				new Product
				{
					Product_Id = 3,
					Title = "TEST3",
					ProductDescription = "3TTTTT",
					Author = "LOL",
					ISBN = "QWE",
					ListPrice = 10,
					Price = 10,
					Price50 = 8,
					Price100 = 6
				}
			);
			#endregion

		}


		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
	}
}
