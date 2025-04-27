using DAL.Entities;
using Microsoft.AspNetCore.Identity;
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
        ICategoryRepository Categories { get; }
        ISubCategoryRepository subCategories { get; }
        IProductRepository products { get; }
        IBrandRepository brands { get; }
        IShoppingCartRepository ShoppingCarts { get; }
        IStoreRepository stores { get; }
        IOrderRepository Orders { get; }
        IOrderDetailsRepository OrderDetails { get; }

        UserManager<AppUser> UserManager { get; }
        SignInManager<AppUser> SignInManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }

        IProductBalanceRepository ProductBalances { get; }

        void save();

        public void Dispose();
    }
}