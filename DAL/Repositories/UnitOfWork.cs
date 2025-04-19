using DAL.Contracts;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;


        //private set
        private readonly Lazy<ICategoryRepository> _CategoryRepository;
        private readonly Lazy<ISubCategoryRepository> _SubCategoryRepository;
        private readonly Lazy<IProductRepository> _ProductRepository;
        private readonly Lazy<IBrandRepository> _BrandRepository;
        private readonly Lazy<IStoreRepository> _StoreRepository;
        private readonly Lazy<IShoppingCartRepository> _ShoppingCartRepository;
        private readonly Lazy<IOrderRepository> _OrderRepository;
        private readonly Lazy<IOrderDetailsRepository> _OrderDetailsRepository;
        public UnitOfWork(AppDbContext appDbContext,UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,SignInManager<AppUser> signInManager
            )
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _CategoryRepository =  new Lazy<ICategoryRepository>(new CategoryRepository(_appDbContext));
            _SubCategoryRepository = new Lazy<ISubCategoryRepository>(new SubCategoryRepository(_appDbContext));
            _ProductRepository = new Lazy<IProductRepository>(new ProductRepository(_appDbContext));
            _BrandRepository = new Lazy<IBrandRepository>(new BrandRepository(_appDbContext));
            _ShoppingCartRepository = new Lazy<IShoppingCartRepository>(new ShoppingCartRepository(_appDbContext));
            _StoreRepository = new Lazy<IStoreRepository>(new StoreRepository(_appDbContext));
            _OrderRepository = new Lazy<IOrderRepository>(new OrderRepository(_appDbContext));
            _OrderDetailsRepository = new Lazy<IOrderDetailsRepository>(new OrderDetailsRepository(_appDbContext));
        }



        public UserManager<AppUser> UserManager => _userManager;
        public SignInManager<AppUser> SignInManager => _signInManager;
        public RoleManager<IdentityRole> RoleManager => _roleManager;
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
        public IOrderRepository Orders
        {
            get
            {
                return _OrderRepository.Value;
            }
        }

        public IOrderDetailsRepository OrderDetails
        {
            get
            {
                return _OrderDetailsRepository.Value;
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
