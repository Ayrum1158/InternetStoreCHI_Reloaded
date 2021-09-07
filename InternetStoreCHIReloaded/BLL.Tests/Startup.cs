using BLL.ConfigPOCOs;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using BLL.Services.AccessTokenGenerators;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Tests
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; } 

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration = new ConfigurationBuilder()
            .AddJsonFile("appconfig.json", false, true)
            .Build();

            var jwtConfigSection = Configuration.GetSection(nameof(JwtConfig));
            services.Configure<JwtConfig>(jwtConfigSection);
            var cartsConfigSection = Configuration.GetSection(nameof(CartsConfig));
            services.Configure<CartsConfig>(cartsConfigSection);

            services.AddDbContext<StoreContext>(x => x.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICartsRepository, CartsRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ICartsService, CartsService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddIdentity<UserEntity, IdentityRole<int>>()
                .AddEntityFrameworkStores<StoreContext>();
            services.AddTransient<UserManager<UserEntity>>();
            services.AddTransient<SignInManager<UserEntity>>();
            services.AddTransient<IAccessTokenGenerator, AccessTokenGenerator>();
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<DbResponse, ServiceResult>().ReverseMap();
                cfg.CreateMap(typeof(DbResponse<>), typeof(ServiceResult<>)).ReverseMap();
                cfg.CreateMap<CategoryEntity, Category>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();
                cfg.CreateMap<ProductEntity, Product>().ReverseMap();
                cfg.CreateMap<ProductsFilter, ProductRequestFilter>().ReverseMap();
                cfg.CreateMap<User, UserEntity>().ReverseMap();
                cfg.CreateMap<UserRegistrationModel, UserEntity>().ReverseMap();
                cfg.CreateMap<NewUserDbModel, UserRegistrationModel>().ReverseMap();
                cfg.CreateMap<NewUserDbModel, UserEntity>().ReverseMap();
                cfg.CreateMap<CartItem, CartItemEntity>().ReverseMap();
                cfg.CreateMap<Cart, CartEntity>().ReverseMap();
                cfg.CreateMap<CartEntity, OrderEntity>()
                .ForMember(oe => oe.OrderedItems, opt => opt.MapFrom(ce => ce.CartItems))
                .ReverseMap();
                cfg.CreateMap<OrderedItem, OrderedItemEntity>().ReverseMap();
                cfg.CreateMap<Order, OrderEntity>().ReverseMap();
            },
            typeof(BLL.Tests.Startup));
        }
    }
}
