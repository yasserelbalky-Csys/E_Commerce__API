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
	internal class ApplicationUserRepository : BaseRepository<Brands>, IBrandRepository
	{
		public ApplicationUserRepository(AppDbContext appDbContext) : base(appDbContext) { }
	}
}