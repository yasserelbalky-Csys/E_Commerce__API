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

        public bool DeleteById(int id)
        {
            var entity = _uof.ProductBalances.GetById(id);
            if (entity == null)
                return false;

            _uof.ProductBalances.Delete(entity.id);
            _uof.save();
            return true;
        }

        public IEnumerable<CurrentProductBalanceListDto> GetAll()
        {
            return _uof.ProductBalances
                 .GetAll()
                 .Select(x => new CurrentProductBalanceListDto
                 {
                     id = x.id,
                     ProductId = x.ProductId,
                     Qty = x.Qty,
                     b_cancel = x.b_cancel,
                     b_pending = x.b_pending,
                     StoreId = x.StoreId,
                     OrderNo = x.OrderNo
                 });
        }

        public CurrentProductBalanceListDto GetById(int id)
        {
            var entity = _uof.ProductBalances.GetById(id);
            if (entity == null)
                return null;

            return new CurrentProductBalanceListDto
            {
                id = entity.id,
                ProductId = entity.ProductId,
                Qty = entity.Qty,
                b_cancel = entity.b_cancel,
                b_pending = entity.b_pending,
                StoreId = entity.StoreId,
                OrderNo = entity.OrderNo
            };
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

        public bool update(CurrentProductBalanceUpdateDto init)
        {
            var entity = _uof.ProductBalances.GetById(init.id);
            if (entity == null)
                return false;

            entity.Qty = init.Qty;
            entity.b_cancel = false;
            entity.b_pending = true;
            entity.ProductId = init.ProductId;
            entity.StoreId = 0;
            entity.OrderNo = 0;

            _uof.ProductBalances.Update(entity);
            _uof.save();
            return true;
        }
    }
}