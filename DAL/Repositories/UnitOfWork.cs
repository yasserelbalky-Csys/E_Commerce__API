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

        //private set
        private readonly Lazy<ICategoryRepository> _CategoryRepository;
        private readonly Lazy<ISubCategoryRepository> _SubCategoryRepository;
        private readonly Lazy<IProductRepository> _ProductRepository;
        private readonly Lazy<IBrandRepository> _BrandRepository;
        private readonly Lazy<IStoreRepository> _StoreRepository;
        private readonly Lazy<IShoppingCartRepository> _ShoppingCartRepository;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _CategoryRepository =  new Lazy<ICategoryRepository>(new CategoryRepository(_appDbContext));
            _SubCategoryRepository = new Lazy<ISubCategoryRepository>(new SubCategoryRepository(_appDbContext));
            _ProductRepository = new Lazy<IProductRepository>(new ProductRepository(_appDbContext));
            _BrandRepository = new Lazy<IBrandRepository>(new BrandRepository(_appDbContext));
            _ShoppingCartRepository = new Lazy<IShoppingCartRepository>(new ShoppingCartRepository(_appDbContext));
            _StoreRepository = new Lazy<IStoreRepository>(new StoreRepository(_appDbContext));
        }
        public ICategoryRepository Categories { get {
                return _CategoryRepository.Value;
            } }
        public ISubCategoryRepository subCategories
        {
            get
            {
                return _SubCategoryRepository.Value;
            }
        }
        public IProductRepository products
        {
            get
            {
                return _ProductRepository.Value;
            }
        }
        public IBrandRepository brands
        {
            get
            {
                return _BrandRepository.Value;
            }
        }
        public IStoreRepository stores
        {
            get
            {
                return _StoreRepository.Value;
            }
        }
        public IShoppingCartRepository ShoppingCarts
        {
            get
            {
                return _ShoppingCartRepository.Value;
            }
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
