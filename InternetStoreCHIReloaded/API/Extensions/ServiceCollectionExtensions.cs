using API.ViewModels;
using BLL.Models;
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
using DAL.Models;
using BLL.Interfaces;
using BLL.Services;

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
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICartsRepository, CartsRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
        }

        public static void ConfigureBLLServices(this IServiceCollection services)
        {
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ICartsService, CartsService>();
            services.AddTransient<IOrdersService, OrdersService>();
        }

        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<ServiceResult, GenericResponse>().ReverseMap();
                cfg.CreateMap(typeof(ServiceResult<>), typeof(GenericResponse<>)).ReverseMap();
                cfg.CreateMap<DbResponse, ServiceResult>().ReverseMap();
                cfg.CreateMap(typeof(DbResponse<>), typeof(ServiceResult<>)).ReverseMap();

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

                cfg.CreateMap<ProductsFilter, ProductsFilterViewModel>().ReverseMap();
                cfg.CreateMap<ProductsFilter, ProductRequestFilter>().ReverseMap();

                cfg.CreateMap<UserRegistrationViewModel, UserRegistrationModel>().ReverseMap();

                cfg.CreateMap<User, UserEntity>().ReverseMap();
                cfg.CreateMap<UserRegistrationModel, UserEntity>().ReverseMap();
                cfg.CreateMap<NewUserDbModel, UserRegistrationModel>().ReverseMap();
                cfg.CreateMap<NewUserDbModel, UserEntity>().ReverseMap();

                cfg.CreateMap<UserLoggingInModel, LoginViewModel>().ReverseMap();

                cfg.CreateMap<CartItem, CartItemEntity>().ReverseMap();

                cfg.CreateMap<AddToCartModel, AddToCartViewModel>().ReverseMap();
                cfg.CreateMap<AddToCartModel, ProductToCartDbModel>().ReverseMap();

                cfg.CreateMap<Cart, CartEntity>().ReverseMap();

                cfg.CreateMap<RemoveFromCartModel, ProductToCartDbModel>().ReverseMap();
                cfg.CreateMap<RemoveFromCartModel, RemoveFromCartViewModel>().ReverseMap();

                cfg.CreateMap<CartEntity, OrderEntity>()
                .ForMember(oe => oe.OrderedProducts, opt => opt.MapFrom(ce => ce.CartItems))
                .ReverseMap();

                cfg.CreateMap<OrderedProduct, OrderedProductEntity>().ReverseMap();
                cfg.CreateMap<Order, OrderEntity>().ReverseMap();
            },
            typeof(Startup));
        }
    }
}
