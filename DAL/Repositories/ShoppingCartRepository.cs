using DAL.Contracts;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	internal class ShoppingCartRepository : BaseRepository<ShoppingCart>, IShoppingCartRepository
	{
		public ShoppingCartRepository(AppDbContext appDbContext) : base(appDbContext) { }

		public override IEnumerable<ShoppingCart> GetAll()
		{
			return _entitySet.Include(prod => prod.Product).AsEnumerable();
		}

		public override ShoppingCart GetById(int id)
		{
			return base.GetById(id);
		}

		public IEnumerable<ShoppingCart> GetByuseridOnly(string userid)
		{
			return _entitySet.Include(sc => sc.Product).Where(sc => sc.UserId == userid).AsEnumerable();
		}

		public int GetCurrentProductBalance(int productid)
		{
			var sql =
				"SELECT SUM(qty) AS Value FROM currentProductBalance where currentProductBalance.ProductId=productid";
			return _appDbContext.Database.SqlQueryRaw<int?>($"{sql}").FirstOrDefault() ?? 0;
		}

		public int GetProductBalanceOrderPending(int productid)
		{
			throw new NotImplementedException();
		}

		public ShoppingCart GetProductByuserid(string userid, int productid)
		{
			return _entitySet.Include(sc => sc.Product) // Include Product if needed
				.FirstOrDefault(sc => sc.UserId == userid && sc.ProductId == productid)!;
		}

		public void InsertProductBalanceRaw(int productId, int qty)
		{
			string sql =
				"INSERT INTO currentProductBalance (ProductId, Qty,StoreId,b_order_done,b_order_Pending) VALUES (@p0, @p1,1,0,0)";
			_appDbContext.Database.ExecuteSqlRaw(sql, productId, qty);
		}
	}
}