using DAL.Contracts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class ProductBalanceRepository : BaseRepository<CurrentProductBalance>, IProductBalanceRepository
    {
        public ProductBalanceRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public int getProductbalance(int productid)
        {
            return _entitySet.Where(x => x.ProductId == productid).Sum(x => x.Qty);
        }
    }
}