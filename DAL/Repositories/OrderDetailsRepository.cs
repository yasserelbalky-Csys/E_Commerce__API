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
    internal class OrderDetailsRepository : BaseRepository<OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public void DeleteOrderDetails(int OrderNo, int LineNo, int ProductId)
        {
            var entity = _entitySet.Find(OrderNo);

            if (entity != null) {
                _entitySet.Remove(entity);
            }
        }

        public override IEnumerable<OrderDetails> GetAll()
        {
            return _entitySet.Include(prod => prod.Product).AsEnumerable();
        }

        public override OrderDetails GetById(int id)
        {
            return base.GetById(id);
        }

        public IEnumerable<OrderDetails> GetByOrderNo(int orderNo)
        {
            return _appDbContext.OrderDetails.Include(prod => prod.Product).Where(od => od.OrderNo == orderNo).ToList();
        }
    }
}