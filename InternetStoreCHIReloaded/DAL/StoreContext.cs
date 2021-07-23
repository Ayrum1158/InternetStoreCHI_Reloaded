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

        public StoreContext(IOptionsMonitor<DBConfig> dbConfig)
        {
            this.dbConfig = dbConfig;

            //Database.Migrate();
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(dbConfig.CurrentValue.ConnectionString);
            }

            //optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
