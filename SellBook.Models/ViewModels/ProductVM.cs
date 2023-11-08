using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SellBook.Models;
namespace SellBook.Models.ViewModels
{
	public class ProductVM
	{
		public Product product { get; set; }
		public IEnumerable<SelectListItem> CategoryList { get; set; }
	}
}
