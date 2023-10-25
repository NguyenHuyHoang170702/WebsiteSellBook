using Microsoft.EntityFrameworkCore;
using SellBook.Models;

namespace SellBook.DataAccess
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public ApplicationDbContext()
		{

		}


		public DbSet<Category> Categories { get; set; }
	}
}
