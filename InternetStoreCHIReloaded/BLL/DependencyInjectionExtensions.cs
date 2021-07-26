using Common.ConfigPOCOs;
using DAL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public static class DependencyInjectionExtensions
    {
        public static void ConfigureDBContext(this IServiceCollection services, DBConfig dbOptions)
        {
            services.AddDbContext<StoreContext>(x => x.UseSqlServer(dbOptions.ConnectionString));
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
