using BLL.Contracts;
using BLL.Services;
using DAL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace BLL
{
    public static class ApplicationServices
    {

        public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISessionManager, SessionManager>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IHelperService, HelperService>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<ICurrentProductBalanceService, CurrentProductBalanceService>();
            return services;
        }
    }
}
