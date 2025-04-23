using BLL.DTOs.OrderDetailsDtos;
using BLL.DTOs.OrderDtos;
using BLL.DTOs.ProductDtos;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IOrderService
    {
        public IEnumerable<OrderListDto> GetOrders();

        public OrderListDto GetOrderById(int id);

        public bool InsertOrder(OrderInsertDto order);

        public int UpdateOrder(OrderUpdateDto order, ICollection<OrderDetailsUpdateDto> details);

        public void DeleteOrder(int id);
    }
}