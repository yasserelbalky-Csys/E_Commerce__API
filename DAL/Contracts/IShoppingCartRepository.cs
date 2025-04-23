using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IShoppingCartRepository : IBaseRepository<ShoppingCart>
    {
        public ShoppingCart GetProductByuserid(string userid, int productid);
        public IEnumerable<ShoppingCart> GetByuseridOnly(string userid);
    }
}
