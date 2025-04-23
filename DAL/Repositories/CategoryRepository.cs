using DAL.Contracts;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class CategoryRepository : BaseRepository<Categories>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public IEnumerable<Categories> GetSubCategoriesByMainCategory(int mainCategoryId)
        {
            return _entitySet.Include(m => m.SubCategories).Where(c => c.CategoryId == mainCategoryId).AsEnumerable();
        }
    }
}