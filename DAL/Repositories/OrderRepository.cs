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
    internal class OrderRepository : BaseRepository<OrderMaster>, IOrderRepository
    {
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public override IEnumerable<OrderMaster> GetAll()
        {
            return _entitySet.Include(order => order.User).AsEnumerable();
        }

        public override OrderMaster GetById(int id)
        {
           return _entitySet.Include(order => order.User).AsEnumerable()
                .FirstOrDefault(order => order.OrderNo == id); 
        }
    }
}
