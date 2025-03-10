using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
                
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Add Roles
            base.OnModelCreating(builder);
            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="ADMIN"
                }
                ,new IdentityRole
                {
                    Name="User",
                    NormalizedName="USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }

      //  public DbSet<Categories> categories {get;set;}

        public DbSet<SubCategories> subCategories {get;set;}


        public DbSet<Products> products {get;set;}


        public DbSet<Brands> brands {get;set;}

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }


    }
}
