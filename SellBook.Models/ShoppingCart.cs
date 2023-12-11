using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.Models
{
	public class ShoppingCart
	{
		#region Shopping Cart Id
		[Key]
		public int Id { get; set; }
		#endregion


		#region Product Id
		public int ProductId { get; set; }
		[ValidateNever]
		[ForeignKey("ProductId")]
		public Product Product { get; set; }
		[Range(1, 1000, ErrorMessage = "Please enter value bettween 1 to 1000")]
		#endregion

		#region count product
		public int Count { get; set; }
		#endregion

		#region user Id
		public string ApplicationUserId { get; set; }
		[ValidateNever]
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }

		#endregion

	}
}
