using Microsoft.AspNetCore.Mvc;

namespace MyTask_Management_System.Core.Repository
{
    public interface ITokenService 
    {
        string CreateToken(AppUser user);

    }
}
