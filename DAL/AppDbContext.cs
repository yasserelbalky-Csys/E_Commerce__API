using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL {
	public class AppDbContext : IdentityDbContext<AppUser> {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder) {
			////Add Roles Run Onlyyy for first Time 
			base.OnModelCreating(builder);
			builder.Entity<OrderDetails>().HasKey(od => new { od.LineNo });
			//List<IdentityRole> roles = new List<IdentityRole>()
			//{
			//    new IdentityRole
			//    {
			//        Name="Admin",
			//        NormalizedName="ADMIN"
			//    }
			//    ,new IdentityRole
			//    {
			//        Name="User",
			//        NormalizedName="USER"
			//    }
			//};
			//builder.Entity<IdentityRole>().HasData(roles);
		}

		//public DbSet<Categories> Categories { get; set; }
		public DbSet<SubCategories> subCategories { get; set; }
		public DbSet<Products> products { get; set; }
		public DbSet<Brands> brands { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<Stores> stores { get; set; }

		//Add User
		public DbSet<AppUser> SysUsers { get; set; }

		//Add Master and Details of Order
		public DbSet<OrderMaster> OrderMaster { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }
	}
}