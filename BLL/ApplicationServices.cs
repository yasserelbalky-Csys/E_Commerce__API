using BLL.Contracts;
using BLL.Services;
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
            return services;
        }


    }
}
