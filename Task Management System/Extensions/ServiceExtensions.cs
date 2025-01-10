

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyTask_Management_System.Data;
using System.Text;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using System.Security.Principal;
using MyTask_Management_System.Data.services;

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

            //APPLY CQRS
            services.AddCors();


            // add services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();


     



            return services;
        }
    }
}
