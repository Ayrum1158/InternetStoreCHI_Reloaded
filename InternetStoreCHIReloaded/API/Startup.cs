using API.Extensions;
using BLL.ConfigPOCOs;
using BLL.Interfaces;
using BLL.Services;
using BLL.Services.AccessTokenGenerators;
using DAL;
using DAL.ConfigPOCOs;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var jwtConfigSection = Configuration.GetSection(nameof(JwtConfig));
            services.Configure<JwtConfig>(jwtConfigSection);
            var jwtConfig = jwtConfigSection.Get<JwtConfig>();

            services.AddIdentity<UserEntity, IdentityRole<int>>()
                .AddEntityFrameworkStores<StoreContext>();

            services.AddAuthentication(o =>
           {
               o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
               o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               o.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
               o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
               o.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
               o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           }).AddJwtBearer(o =>
           {
               o.TokenValidationParameters = new TokenValidationParameters()
               {
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),
                   ValidIssuer = jwtConfig.Issuer,
                   ValidAudience = jwtConfig.Audience,
                   ValidateIssuerSigningKey = true,
                   ValidateIssuer = true,
                   ValidateAudience = true
               };
           });

            services.ConfigureDBContext(dbConfig);

            services.ConfigureAutomapper();

            services.ConfigureRepositories();// generic and non-generic repositories

            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IUsersService, UsersService>();

            services.AddTransient<UserManager<UserEntity>>();
            services.AddTransient<SignInManager<UserEntity>>();

            services.AddTransient<IAccessTokenGenerator, AccessTokenGenerator>();

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

            if (env.IsDevelopment() || env.EnvironmentName == "Local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
