using API.ViewModels;
using BLL;
using BLL.Contracts;
using BLL.Interfaces;
using BLL.Services;
using Common.ConfigPOCOs;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSession((options) =>
            //{
            //    options.Cookie.IsEssential = true;
            //});

            var dbConfigSection = Configuration.GetSection(nameof(DBConfig));
            services.Configure<DBConfig>(dbConfigSection);// add DBConfig to IOptionsManager

            var dbConfig = dbConfigSection.Get<DBConfig>();
            services.ConfigureDBContext(dbConfig);

            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Category, CategoryVM>().ReverseMap();
                cfg.CreateMap<CategoryEntity, Category>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CategoryDescription, opt => opt.MapFrom(src => src.Description))
                //.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                //.ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate));
                .ReverseMap();

            },
            typeof(Startup));

            services.ConfigureRepositories();// generic and non-generic repositories

            services.AddTransient<ICategoryService, CategoryService>();

            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Category API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
