using Cifra.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace Cifra.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Manufacturer_Product
            modelBuilder.Entity<Manufacturer_Product>().HasKey(dg => new
            {
                dg.ManufacturerId,
                dg.ProductId
            });
            modelBuilder.Entity<Manufacturer_Product>().HasOne(g => g.Product).WithMany(dg => dg.Manufacturer_Product).HasForeignKey(g => g.ProductId);
            modelBuilder.Entity<Manufacturer_Product>().HasOne(g => g.Manufacturer).WithMany(dg => dg.Manufacturer_Product).HasForeignKey(g => g.ManufacturerId);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Manufacturer_Product> Manufacturers_Products { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<View> Views { get; set; }

        // Orders related tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<CartItem> CartItems{ get; set; }
    }
}

