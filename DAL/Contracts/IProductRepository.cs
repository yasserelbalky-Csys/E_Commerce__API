using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IProductRepository : IBaseRepository<Products>
    {
        public IEnumerable<Products> GetProductsByCategory();
    }
}