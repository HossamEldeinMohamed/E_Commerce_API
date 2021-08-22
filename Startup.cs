using E_Commerce_API.Models;
using E_Commerce_Api.Interface;
using E_Commerce_Api.Servicies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Api.DTO.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce_Api
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
            services.AddAutoMapper(typeof(Startup));

            services.AddIdentity<ApplicationUsers, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
            }
            ).AddEntityFrameworkStores<E_CommerceContext>();
            //services.AddDefaultIdentity<IdentityUser>()
            //        .AddEntityFrameworkStores<E_CommerceContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "E_Commerce_Api", Version = "v1" });
            });

            services.AddDbContext<E_CommerceContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnecion"));
            });
            services.AddScoped<IAuthenticate, AuthenticateServer>();
            services.AddScoped<ICategory, CategoryService>();
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IProduct, Productservice>();
            services.AddScoped<IOrder, OrderService>();
            services.AddScoped<IOrderDetail, OrderDetailService>();
            services.AddScoped<IRole, RoleService>();
            services.AddControllers(
            ).AddNewtonsoftJson(options =>
           options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            var appsttingSection = Configuration.GetSection("AppSettings");
            var appStting = appsttingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appStting.Secret);
            services.Configure<AppSettings>(appsttingSection);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }
            
  

        //This method gets called by the runtime.Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "E_Commerce_Api v1"));

            }
           
            app.UseHttpsRedirection();

            
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
