using API.ViewModels;
using BLL.Contracts;
using Common.ConfigPOCOs;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDBContext(this IServiceCollection services, DBConfig dbOptions)
        {
            services.AddDbContext<StoreContext>(x => x.UseSqlServer(dbOptions.ConnectionString));
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }

        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Category, CategoryViewModel>().ReverseMap();

                cfg.CreateMap<CategoryEntity, Category>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();

                cfg.CreateMap<ResultContract, GenericResponse>().ReverseMap();

                cfg.CreateMap(typeof(ResultContract<>), typeof(GenericResponse<>)).ReverseMap();
            },
            typeof(Startup));
        }
    }
}
