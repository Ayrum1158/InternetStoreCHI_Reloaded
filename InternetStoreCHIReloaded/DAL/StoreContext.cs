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

        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<ProductWithQuantityEntity> ProductsWithQuantity { get; set; }
        public virtual DbSet<ProductsSetEntity> ProductsSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _dbConfig.CurrentValue.ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }

            //optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryEntity>().Property(ce => ce.Name).HasMaxLength(20);
            modelBuilder.Entity<CategoryEntity>().Property(ce => ce.Description).HasMaxLength(200);
            modelBuilder.Entity<CategoryEntity>().HasKey(ce => ce.Id);

            modelBuilder.Entity<ProductEntity>().HasKey(pe => pe.Id);
            modelBuilder.Entity<ProductEntity>().HasOne(pe => pe.Category).WithMany(ce => ce.Products).HasForeignKey(pe => pe.CategoryId);

            modelBuilder.Entity<UserEntity>().HasOne(u => u.UserCart).WithMany();
            modelBuilder.Entity<UserEntity>().HasMany(u => u.UserOrders).WithMany(ps => ps.Users);

            modelBuilder.Entity<ProductsSetEntity>().HasMany(ps => ps.Products).WithMany(pwq => pwq.ProductsSets);

            modelBuilder.Seed();
        }
    }
}
