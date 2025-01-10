


using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyTask_Management_System.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<TaskModel> TaskModels { get; set; }
    }
}
