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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens; // <-- ADD THIS

namespace DAL
{
	public static class DataAcessExtensions
	{
		public static IServiceCollection AddDataAccessServices(this IServiceCollection services,
			IConfigurationManager config)
		{
			string cs = config.GetConnectionString("DefaultConnection");
			services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(cs); });
			//test unit of work

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			return services;
		}

        public static IServiceCollection AddAppIdentity(this IServiceCollection services,
           IConfigurationManager config)
        {


            //Identity
            services.AddIdentity<AppUser, IdentityRole>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            }).AddEntityFrameworkStores<AppDbContext>() // Link Identity to AppDbContext
                .AddDefaultTokenProviders();
            ;



            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = options.DefaultChallengeScheme = options.DefaultForbidScheme =
                    options.DefaultScheme = options.DefaultSignInScheme =
                        options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = config["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = config["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(config["JWT:SigningKey"]))
                };
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

    }
}