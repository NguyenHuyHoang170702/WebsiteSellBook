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


		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
	}
}
