using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Entities;

namespace DAL.Repositories
{
	internal class CategoryRepository : BaseRepository<Categories>, ICategoryRepository
	{
		public CategoryRepository(AppDbContext appDbContext) : base(appDbContext) { }
	}
}