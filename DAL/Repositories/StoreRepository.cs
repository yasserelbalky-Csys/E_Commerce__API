using DAL.Contracts;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	internal class StoreRepository : BaseRepository<Stores>, IStoreRepository
	{
		public StoreRepository(AppDbContext appDbContext) : base(appDbContext) { }

		public override IEnumerable<Stores> GetAll()
		{
			return base.GetAll();
		}

		public override Stores GetById(int id)
		{
			return base.GetById(id);
		}
	}
}