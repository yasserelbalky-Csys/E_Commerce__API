using BLL.Contracts;
using BLL.DTOs.CurrentProductBalanceDtos;
using DAL.Contracts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class CurrentProductBalanceService : ICurrentProductBalanceService
    {
        private readonly IUnitOfWork _uof;

        public CurrentProductBalanceService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public bool Insert(CurrentProductBalanceInsertDto init)
        {
            if (init.Qty <= 0)
            {
                return false;
            }
            else
            {
                _uof.ProductBalances.Insert(new CurrentProductBalance
                {
                    OrderNo = 0,
                    Qty = init.Qty,
                    b_cancel = false,
                    b_pending = true,
                    ProductId = init.ProductId,
                    StoreId = 0
                });
                _uof.save();
                return true;
            }
        }
    }
}