using E_Commerce.Core.Entitiies;
using E_Commerce.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commece.DA.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().HasMany(o=>o.Products).WithOne(p=>p.Order)
                .HasForeignKey(p=>p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product A", Price = 10.99m, Quantity = 5,OrderId = 1 },
                new Product { Id = 2, Name = "Product B", Price = 20.50m, Quantity = 3 ,OrderId=2},
                new Product { Id = 3, Name = "Product C", Price = 15.75m, Quantity = 2,OrderId=1 }
            );


            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    OrderDate = new DateTime(2025, 5, 1),
                    Status = OrderStatus.Pending
                 
                },
                new Order
                {
                    Id = 2,
                    OrderDate = new DateTime(2025, 5, 2),
                    Status = OrderStatus.Completed
                  
                }
            );

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
