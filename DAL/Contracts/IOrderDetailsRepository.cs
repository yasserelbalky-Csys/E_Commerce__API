using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IOrderDetailsRepository : IBaseRepository<OrderDetails>
    {
        void DeleteOrderDetails(int OrderNo, int LineNo, int ProductId);
    }
}