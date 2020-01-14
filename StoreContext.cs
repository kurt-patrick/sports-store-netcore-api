using System;
using Microsoft.EntityFrameworkCore;
using SportsStoreApi.Entities;

namespace SportsStoreApi
{
    public class StoreContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductName).IsRequired();
                entity.Property(e => e.ProductPrice).IsRequired();
                entity.Property(e => e.Gender).HasColumnType<Gender>("ENUM('Mens', 'Womens')");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique(true);
            });

            modelBuilder.Entity<Order>().HasMany(m => m.Items);
            modelBuilder.Entity<Product>().HasData(SeedData.ProductSeedData.Products);
            modelBuilder.Entity<User>().HasData(SeedData.UserSeedData.Users);
        }

    }

}
