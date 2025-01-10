using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyTask_Management_System.Core.Helper;
using MyTask_Management_System.Data;
using System;
using System.Text;

namespace MyTask_Management_System.Extensions
{
    public static class IdentityServicesExtensision
    {

        public static IServiceCollection AddIdentityServicesExtensions(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
                .AddEntityFrameworkStores<ApplicationDBContext>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(option =>
                            {
                                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                                {
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
                                    ValidateIssuer = false,
                                    ValidateAudience = false
                                };

                            });


            services.AddAuthorization(op =>
            {
                op.AddPolicy("RequireAdminRole", policy => policy.RequireRole(ConstRole.ADMIN));
                op.AddPolicy("ModerateRole", policy => policy.RequireRole(ConstRole.ADMIN, ConstRole.MODERATOR));
            });
            return services;
        }


    }
}
