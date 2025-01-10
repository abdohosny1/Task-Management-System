

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyTask_Management_System.Data;
using System.Text;
using System;
using MyTask_Management_System.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using System.Security.Principal;

namespace MyTask_Management_System.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services,
         WebApplicationBuilder builder)
        {
            //add db context
            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDBContext>(
                 op => op.UseSqlServer(connection));

            // Register Identity services
    services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDBContext>()
            .AddDefaultTokenProviders();


            // add services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();


     



            return services;
        }
    }
}
