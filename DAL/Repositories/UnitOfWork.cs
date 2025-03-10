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
    class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        //protected readonly DbSet<Entity> _entitySet;


       
        public ICategoryRepository Category { get; private set; }
        public ISubCategoryRepository subCategories { get; private set; }


        public IProductRepository products { get; private set; }


        public IBrandRepository brands { get; private set; }

        public IShoppingCartRepository ShoppingCarts { get; private set; }
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            Category = new CategoryRepository(_appDbContext);
            subCategories =new SubCategoryRepository(appDbContext);
            products = new ProductRepository(appDbContext);
            brands = new BrandRepository(appDbContext);
            ShoppingCarts = new ShoppingCartRepository(appDbContext);

        }


        public void save()
        {
            _appDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
