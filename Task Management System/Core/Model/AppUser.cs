

using Microsoft.AspNetCore.Identity;

namespace MyTask_Management_System.Core.Model
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string Name { get; set; } 
        public ICollection<AppUserRole> UserRoles { get; set; }

    }
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }

    public class AppUserRole : IdentityUserRole<int>
    {
        public AppUser User { get; set; }
        public AppRole Role { get; set; }
    }

}
