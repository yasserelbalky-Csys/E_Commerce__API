using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface ICategoryRepository : IBaseRepository<Categories>
    {
        public IEnumerable<Categories> GetSubCategoriesByMainCategory(int mainCategoryId);


    }
}