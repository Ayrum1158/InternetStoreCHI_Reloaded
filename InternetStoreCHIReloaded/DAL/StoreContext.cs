using Common.ConfigPOCOs;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class StoreContext : DbContext
    {
        private readonly IOptionsMonitor<DBConfig> dbConfig;

        public StoreContext(DbContextOptions<StoreContext> options, IOptionsMonitor<DBConfig> dbConfig) : base(options)
        {
            this.dbConfig = dbConfig;

            Database.Migrate();
        }

        public virtual DbSet<CategoryEntity> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = dbConfig.CurrentValue.ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }

            //optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
