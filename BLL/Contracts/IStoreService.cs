using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.CategoryDTOs;
using BLL.DTOs.StoreDtos;

namespace BLL.Contracts
{
	public interface IStoreService
	{
		public IEnumerable<StoreListDto> GetStores();
		public StoreListDto GetStore(int id);
		public void InsertStore(StoreInsertDto store);
		public void UpdateStore(StoreUpdateDto store);
		public void DeleteStore(int id);
	}
}