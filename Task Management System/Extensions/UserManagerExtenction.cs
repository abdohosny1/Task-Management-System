using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MyTask_Management_System.Extensions
{
    public static class UserManagerExtenction
    {
        public static async Task<AppUser> FindByEmailWithAddress(
            this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            // var email = user?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.SingleOrDefaultAsync(y => y.UserName == email);
        }

        public static async Task<AppUser> FindByEmailClaimsPrincipal(this UserManager<AppUser> input, ClaimsPrincipal user)
        {


            var email = user.FindFirstValue(ClaimTypes.Email);
            // var email = user?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.SingleOrDefaultAsync(y => y.UserName == email);
        }
    }
}
