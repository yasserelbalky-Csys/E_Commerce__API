using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
	internal class BrandRepository : BaseRepository<Brands>, IBrandRepository
	{
		public BrandRepository(AppDbContext appDbContext) : base(appDbContext) { }

		public override IEnumerable<Brands> GetAll() {
			return base.GetAll();
		}

		//public IEnumerable<Brands> getBrandsWithItsProducts(int brandid)
		//{
		//    return _entitySet.Include(p=>p.Products).AsEnumerable();
		//}

		public override Brands GetById(int id) {
			return base.GetById(id);
		}
	}
}