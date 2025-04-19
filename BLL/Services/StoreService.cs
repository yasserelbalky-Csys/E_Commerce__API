using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Contracts;
using BLL.DTOs.StoreDtos;
using DAL.Contracts;
using DAL.Entities;


namespace BLL.Services {
	internal class StoreService : IStoreService {
		private readonly IUnitOfWork _uof;

		public StoreService(IUnitOfWork uof) {
			_uof = uof;
		}

		public void DeleteStore(int id) {
			var store = _uof.stores.GetById(id);
			if (store != null) {
				store.b_deleted = true;
			} else {
				throw new KeyNotFoundException($"Store with ID {id} not found.");
			}

			_uof.save();
		}

		public StoreListDto GetStore(int id) {
			var found = _uof.stores.GetById(id);
			if (found != null) {
				return new StoreListDto {
					StoreId = found.StoreId,
					StoreName = found.StoreName,
					b_deleted = found.b_deleted,
				};
			} else {
				return null;
			}
		}

		public IEnumerable<StoreListDto> GetStores() {
			return _uof.stores.GetAll().Select(store => new StoreListDto {
				StoreId = store.StoreId,
				StoreName = store.StoreName
			}).Where(storee => storee.b_deleted == false);
		}

		public void InsertStore(StoreInsertDto store) {
			_uof.stores.Insert(new Stores {
				StoreName = store.StoreName,
				b_deleted = false
			});
			_uof.save();
		}

		public void UpdateStore(StoreUpdateDto store) {
			var found = _uof.stores.GetById(store.StoreId);
			if (found != null) {
				found.StoreId = store.StoreId;
				found.StoreName = store.StoreName;
				found.b_deleted = store.b_deleted;
				_uof.stores.Update(found);
			} else {
				throw new KeyNotFoundException($"Store with ID {store.StoreId} not found.");
			}

			_uof.save();
		}
	}
}