using DAL.Entities;
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





        public DbSet<Categories> categories {get;set;}

        public DbSet<SubCategories> subCategories {get;set;}


        public DbSet<Products> products {get;set;}


        public DbSet<Brands> brands {get;set;}




    }
}
