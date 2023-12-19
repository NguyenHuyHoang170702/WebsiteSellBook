using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SellBook.Models;

namespace SellBook.DataAccess
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
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
			base.OnModelCreating(modelBuilder);

			#region Add sample data for Category
			modelBuilder.Entity<Category>().HasData(
					new Category { Category_ID = 1, Category_Name = "Comestic", DisplayOrder = 1 },
					new Category { Category_ID = 2, Category_Name = "Family", DisplayOrder = 1 },
					new Category { Category_ID = 3, Category_Name = "History", DisplayOrder = 1 }
				);
			#endregion

			#region Add sample data for Product
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					Product_Id = 1,
					Title = "TEST1",
					ProductDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
					Author = "LOL",
					ISBN = "QWE",
					ListPrice = 10,
					Price = 10,
					Price50 = 8,
					Price100 = 6,
					CategoryId = 2,
					ProductImageUrl = "",
				},
				new Product
				{
					Product_Id = 2,
					Title = "TEST2",
					ProductDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
					Author = "LOL",
					ISBN = "QWE",
					ListPrice = 10,
					Price = 10,
					Price50 = 8,
					Price100 = 6,
					CategoryId = 1,
					ProductImageUrl = "",
				},
				new Product
				{
					Product_Id = 3,
					Title = "TEST3",
					ProductDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
					Author = "LOL",
					ISBN = "QWE",
					ListPrice = 10,
					Price = 10,
					Price50 = 8,
					Price100 = 6,
					CategoryId = 2,
					ProductImageUrl = "",
				}
			);
			#endregion

			#region Add sample data for Company
			modelBuilder.Entity<Company>().HasData(
				new Company { CpmpanyId = 1, CompanyName = "Company1", City = "Hanoi", PhoneNumber = "0123456789", PostalCode = "1234", State = "KKKK", StressAddress = "N/A" },
				new Company { CpmpanyId = 2, CompanyName = "Company2", City = "HoChiMinh", PhoneNumber = "0198756789", PostalCode = "3456", State = "YYYY", StressAddress = "N/A" },
				new Company { CpmpanyId = 3, CompanyName = "Company3", City = "DaNang", PhoneNumber = "0123456123", PostalCode = "1298", State = "BBBB", StressAddress = "N/A" }

				);
			#endregion

		}


		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }

		public DbSet<OrderHeader> OrderHeaders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
	}
}
