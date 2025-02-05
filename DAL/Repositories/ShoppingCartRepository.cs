using DAL.Contracts;
using DAL.Entities;
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
            return base.GetAll();
        }


        public override ShoppingCart GetById(int id)
        {
            return base.GetById(id);
        }
    }
}
