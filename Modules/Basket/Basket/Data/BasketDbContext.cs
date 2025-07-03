using EShop.Basket.Basket.Features.Models;
using MassTransit.Middleware;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Basket.Data
{
    public class BasketDbContext : DbContext
    {
        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options)
        {
        }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Basket"); // Set the default schema for the database context
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BasketDbContext).Assembly); // Automatically apply configurations from the assembly
            // Configure your entities here, e.g. modelBuilder.Entity<YourEntity>().ToTable("YourTableName");
        }
        // Define DbSet properties for your entities, e.g. public DbSet<ShoppingCart> ShoppingCarts { get; set;
    }
}
