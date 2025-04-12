using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories {
	internal class SubCategoryRepository : BaseRepository<SubCategories>, ISubCategoryRepository {
		public SubCategoryRepository(AppDbContext appDbContext) : base(appDbContext) { }

		public override IEnumerable<SubCategories> GetAll() {
			//    //return base.GetAll();
			return _entitySet.Include(cat => cat.Category).AsEnumerable();
		}

		public override SubCategories GetById(int id) {
			return _entitySet.Include(cat => cat.Category).FirstOrDefault(cat => cat.SubCategoryId == id)!;
		}
	}
}