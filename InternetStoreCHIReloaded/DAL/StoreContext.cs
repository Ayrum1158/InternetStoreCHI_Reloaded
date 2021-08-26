using DAL.ConfigPOCOs;
using DAL.Entities;
using DAL.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class StoreContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
    {
        private readonly IOptionsMonitor<DBConfig> _dbConfig;

        public StoreContext(DbContextOptions<StoreContext> options, IOptionsMonitor<DBConfig> dbConfig) : base(options)
        {
            _dbConfig = dbConfig;
        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CartItemEntity> Cartitems { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<OrderedProductEntity> OrderedProducts { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _dbConfig.CurrentValue.ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryEntity>().Property(ce => ce.Name).HasMaxLength(20);
            modelBuilder.Entity<CategoryEntity>().Property(ce => ce.Description).HasMaxLength(200);
            modelBuilder.Entity<CategoryEntity>().HasKey(ce => ce.Id);

            modelBuilder.Entity<ProductEntity>().HasKey(pe => pe.Id);
            modelBuilder.Entity<ProductEntity>().HasOne(pe => pe.Category).WithMany(ce => ce.Products).HasForeignKey(pe => pe.CategoryId);

            modelBuilder.Entity<UserEntity>().HasOne(u => u.UserCart).WithOne(c => c.User).HasForeignKey<CartEntity>(c => c.UserId);

            modelBuilder.Entity<CartItemEntity>().HasOne(ci => ci.Product).WithMany().HasForeignKey(ci => ci.ProductId);

            modelBuilder.Entity<CartEntity>().HasMany(ce => ce.CartItems).WithOne().HasForeignKey(cie => cie.CartId);

            modelBuilder.Entity<OrderEntity>().HasMany(oe => oe.OrderedProducts).WithOne().HasForeignKey(ope => ope.OrderId);

            modelBuilder.Entity<UserEntity>().HasMany(ue => ue.UserOrders).WithOne().HasForeignKey(oe => oe.UserId);

            modelBuilder.Entity<OrderedProductEntity>().HasOne(ope => ope.Product).WithMany().HasForeignKey(ope => ope.ProductId);

            modelBuilder.Seed();
        }
    }
}
