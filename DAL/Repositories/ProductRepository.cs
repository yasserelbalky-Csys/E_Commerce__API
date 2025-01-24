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
    internal class ProductRepository : BaseRepository<Products>, IProductRepository
    {

        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public override IEnumerable<Products> GetAll()
        {
            return _entitySet.Include(prod=>prod.Subcategory).AsEnumerable();
        }

        public override Products GetById(int id)
        {
            return _entitySet.Include(prod => prod.Subcategory).FirstOrDefault(prod=>prod.ProductId==id);
        }

        public IEnumerable<Products> GetProductsByCategory()
        {
            throw new NotImplementedException();
        }
    }
}
