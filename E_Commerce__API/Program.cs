using BLL;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace E_Commerce__API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddSwaggerGen(option => {
				option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
				option.AddSecurityDefinition("Bearer",
					new OpenApiSecurityScheme {
						In = ParameterLocation.Header,
						Description = "Please enter a valid token",
						Name = "Authorization",
						Type = SecuritySchemeType.Http,
						BearerFormat = "JWT",
						Scheme = "Bearer"
					});
				option.AddSecurityRequirement(new OpenApiSecurityRequirement {
					{
						new OpenApiSecurityScheme {
							Reference = new OpenApiReference {
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] { }
					}
				});
			});

			builder.Services.AddDistributedMemoryCache(); // Required for session
			builder.Services.AddSession(options => {
				options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});
			builder.Services.AddHttpContextAccessor(); // Required to access HttpContext
			//End Session

			builder.Services.AddSession();

			builder.Services.AddApplicationLayerServices();

			builder.Services.AddDataAccessServices(builder.Configuration);
			builder.Services.AddAppIdentity(builder.Configuration);
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment()) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseSession(); // Enable session middleware
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();
			app.Run();
		}
	}
}