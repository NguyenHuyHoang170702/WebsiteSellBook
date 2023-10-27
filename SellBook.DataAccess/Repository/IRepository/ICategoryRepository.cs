using SellBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBook.DataAccess.Repository.IRepository
{
	// implement defines functions to IRepository and extend more function of ICategoryRepository
	public interface ICategoryRepository : IRepository<Category>
	{
		void Update(Category category);
	}
}
