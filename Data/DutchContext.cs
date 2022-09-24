using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchContext : IdentityDbContext<StoreUser>
    {
        //private readonly IConfiguration _config;
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<StoreUser> StoreUser { get; set; }

        
        public DutchContext(DbContextOptions<DutchContext>options) : base(options)
        {
            //_config = config;
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(_config["ConnectionStrings:DutchContextDb"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                OrderNumber = "1"
            });
        }
        
        
    }

    
}
