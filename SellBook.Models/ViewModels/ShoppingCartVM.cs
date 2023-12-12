using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.Models.ViewModels
{
	public class ShoppingCartVM
	{

		public IEnumerable<ShoppingCart> shoppingCartList { get; set; }

		public double OrderTotal { get; set; }
	}
}
