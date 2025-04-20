using DAL.Contracts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	internal class CategoryRepository : BaseRepository<Categories>, ICategoryRepository
	{
		public CategoryRepository(AppDbContext appDbContext) : base(appDbContext) { }
	}
}