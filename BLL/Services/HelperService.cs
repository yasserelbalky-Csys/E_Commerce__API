using BLL.Contracts;
using BLL.DTOs.OrderDtos;
using DAL.Contracts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class HelperService : IHelperService
    {
        private readonly IUnitOfWork _uof;

        public HelperService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public int AddSalesReturnQtyToProductBalance(int orderno)
        {
            var found = _uof.Orders.GetById(orderno);

            if (found == null)
            {
                return 0;
            }
            else
            {
                var listdetails = _uof.OrderDetails.GetByOrderNo(found.OrderNo);
                foreach (var item in listdetails)
                {
                    _uof.ProductBalances.Insert(new CurrentProductBalance
                    {
                        OrderNo = found.OrderNo,
                        ProductId = item.ProductId,
                        Qty = item.Qty,
                        b_cancel = true
                    });
                }
                return 1;
            }
        }

        //make order status is true
        public int UpdateOrderStatus(int order_no)
        {
            var found = _uof.Orders.GetById(order_no);
            if (found != null)
            {
                found.OrderStatus = "Confirmed";
                _uof.save();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int AddPendingOrderQty(int order_no)
        {
            var found = _uof.Orders.GetById(order_no);
            if (found != null)
            {
                var listdetails = _uof.OrderDetails.GetByOrderNo(found.OrderNo);
                foreach (var item in listdetails)
                {
                    _uof.ProductBalances.Insert(new CurrentProductBalance
                    {
                        OrderNo = found.OrderNo,
                        ProductId = item.ProductId,
                        Qty = item.Qty * -1,
                        b_pending = true
                    });
                }
                _uof.save();
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}