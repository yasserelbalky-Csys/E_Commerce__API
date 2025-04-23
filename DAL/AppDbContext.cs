using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderDetails>()
          .HasKey(od => new { od.LineNo });

            base.OnModelCreating(builder);
            builder.Entity<CurrentProductBalance>()
          .HasNoKey();


            builder.Entity<IdentityRole>().HasData(
             new IdentityRole
             {
                 Name = "Admin",
                 NormalizedName = "ADMIN",
                 Id = "64ce2ffc-1b98-40aa-bfd2-a9fa7ab4e26e",
                 ConcurrencyStamp = "admin-role"
             },
         new IdentityRole
         {
             Name = "User",
             NormalizedName = "USER",
             Id = "7a6c7350-b5db-4e15-a6de-c9e68e4d9d7f",
             ConcurrencyStamp = "user-role"
         }
            );



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
        public DbSet<CurrentProductBalance> CurrentProductBalance { get; set; }
    }
}
