using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ISubCategoryRepository subCategories { get;  }


        IProductRepository products { get;  }


        IBrandRepository brands { get;  }

        IShoppingCartRepository ShoppingCarts { get; }
        IStoreRepository stores { get; }
        void save();
        public void Dispose();
    }
}
