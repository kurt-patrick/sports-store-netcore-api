using System;
using Microsoft.EntityFrameworkCore;
using SportsStoreApi.Entities;

namespace SportsStoreApi
{
    public class StoreContext : DbContext
    {
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
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<Product>().HasData(SeedData.ProductSeedData.Products);
            modelBuilder.Entity<User>().HasData(SeedData.UserSeedData.Users);
        }

    }

}
