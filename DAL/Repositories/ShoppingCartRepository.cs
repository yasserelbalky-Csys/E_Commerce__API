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
    internal class ShoppingCartRepository : BaseRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public override IEnumerable<ShoppingCart> GetAll()
        {
            return _entitySet.Include(prod => prod.Product).AsEnumerable();
        }


        public override ShoppingCart GetById(int id)
        {
            return base.GetById(id);
        }

        public ShoppingCart GetByuserid(string userid, int productid)
        {
           return _entitySet
        .Include(sc => sc.Product) // Include Product if needed
        .FirstOrDefault(sc => sc.UserId==userid && sc.ProductId==productid);
        }
    }
}
