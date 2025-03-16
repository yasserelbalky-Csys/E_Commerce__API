using BLL.Contracts;
using BLL.DTOs.StoreDtos;
using DAL.Contracts;
using DAL.Entities;
using DAL.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Services
{
    internal class StoreService : IStoreService
    {
        private readonly IUnitOfWork _uof;

        public StoreService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public void DeleteStore(int id)
        {
            _uof.stores.Delete(id);
            _uof.save();
        }

        public StoreListDto GetStore(int id)
        {
            var found = _uof.stores.GetById(id);
            if (found != null)
            {
                return new StoreListDto
                {

                    StoreId = found.StoreId,
                    StoreName = found.StoreName
                };
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<StoreListDto> GetStores()
        {
            return _uof.stores.GetAll().Select( store=>
                new StoreListDto
                {
                    StoreId=store.StoreId,
                    StoreName=store.StoreName
                }
                );
        }

        public void InsertStore(StoreInsertDto store)
        {
            _uof.stores.Insert(new Stores
            {
                StoreName = store.StoreName
            });
            _uof.save();
        }

        public void UpdateStore(StoreUpdateDto store)
        {
            _uof.stores.Update(new Stores
            {
                StoreId = store.StoreId,
                StoreName = store.StoreName
            });
            _uof.save();
        }
    }
}
