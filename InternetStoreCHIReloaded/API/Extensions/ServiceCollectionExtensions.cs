using API.ViewModels;
using BLL.Contracts;
using DAL.ConfigPOCOs;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repositories;

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

            services.AddTransient<IProductsRepository, ProductsRepository>();
        }

        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<ServiceResult, GenericResponse>().ReverseMap();
                cfg.CreateMap(typeof(ServiceResult<>), typeof(GenericResponse<>)).ReverseMap();

                // Category mapping:

                cfg.CreateMap<Category, CategoryViewModel>().ReverseMap();

                cfg.CreateMap<CategoryEntity, Category>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();

                // Product mapping:

                cfg.CreateMap<Product, ProductViewModel>().ReverseMap();

                cfg.CreateMap<ProductEntity, Product>().ReverseMap();
            },
            typeof(Startup));
        }
    }
}
