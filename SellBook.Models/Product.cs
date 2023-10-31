using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.Models
{
	public class Product
	{
		#region ID
		[Key] public int Product_Id { get; set; }
		#endregion

		#region Title
		[Required(ErrorMessage = "Title is required")]
		[Display(Name = "Title")]
		public string Title { get; set; }
		#endregion

		#region Description
		[Required(ErrorMessage = "Description is required")]
		[Display(Name = "Description")]
		public string ProductDescription { get; set; }
		#endregion

		#region ISBN
		[Required(ErrorMessage = "ISBN is required")]
		[Display(Name = "ISBN")]
		public string ISBN { get; set; }
		#endregion

		#region Author
		[Required(ErrorMessage = "Author is required")]
		[Display(Name = "Author")]
		public string Author { get; set; }
		#endregion

		#region List Price
		[Required(ErrorMessage = "Price is required")]
		[Display(Name = "List Price")]
		[Range(1, 1000)]
		public double ListPrice { get; set; }
		#endregion

		#region Price 1 - 50
		[Required(ErrorMessage = "Price is required")]
		[Display(Name = "Price for 1 - 50")]
		[Range(1, 1000)]
		public double Price { get; set; }
		#endregion

		#region Price 50+ 
		[Required(ErrorMessage = "Price is required")]
		[Display(Name = "Price for 50+")]
		[Range(1, 1000)]
		public double Price50 { get; set; }
		#endregion

		#region Price 100+
		[Required(ErrorMessage = "Price is required")]
		[Display(Name = "Price for 100+")]
		[Range(1, 1000)]
		public double Price100 { get; set; }
		#endregion

	}
}
