using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contracts {
	public interface IUnitOfWork {
		ICategoryRepository Categories { get; }
		ISubCategoryRepository subCategories { get; }
		IProductRepository products { get; }
		IBrandRepository brands { get; }
		IShoppingCartRepository ShoppingCarts { get; }
		IStoreRepository stores { get; }
		IOrderRepository Orders { get; }
		IOrderDetailsRepository OrderDetails { get; }
		void save();
		public void Dispose();
	}
}