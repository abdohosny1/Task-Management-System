using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MyTask_Management_System.Extensions
{
    public static class IdentityServicesExtensision
    {

        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            var bulider = services.AddIdentityCore<AppUser>();

            bulider = new IdentityBuilder(bulider.UserType, bulider.Services);
            bulider.AddEntityFrameworkStores<ApplicationDBContext>();
            bulider.AddSignInManager<SignInManager<AppUser>>();
            //  bulider.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();

            bulider.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                  option =>
                  {
                      option.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                          ValidIssuer = configuration["Token:Issuer"],
                          ValidateIssuer = true,
                          ValidateAudience = false,
                          // ValidateLifetime = true,
                          // ValidAudience = configuration["Token:Audience"],

                      };
                  });
            return services;

        }

    }
}
