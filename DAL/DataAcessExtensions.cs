using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
	public static class DataAcessExtensions
	{
		public static IServiceCollection AddDataAccessServices(this IServiceCollection services,
			IConfigurationManager config) {
			string cs = config.GetConnectionString("DefaultConnection");
			services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(cs); });

			//services.AddScoped<ICategoryRepository, CategoryRepository>();
			//services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
			//services.AddScoped<IProductRepository, ProductRepository>();
			//services.AddScoped<IBrandRepository, BrandRepository>();
			//services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
			//test unit of work
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;
		}
	}
}