using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTask_Management_System.Core.Helper;
using MyTask_Management_System.Dto;

namespace MyTask_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;


        public AcountController(
       UserManager<AppUser> userManager,
       ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await _userManager.
                CheckPasswordAsync(user, loginDto.Password);

            if (!result) return Unauthorized("Invalid Password");

            return new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                Token =await _tokenService.CreateToken(user),
            };
        }
        [HttpGet("Emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;

        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult("Emaill Address is in Use");
            }
            var user = new AppUser
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest();

          //  var rolesResult = await _userManager.AddToRoleAsync(user, ConstRole.MRMBER);

            //if (!rolesResult.Succeeded) return BadRequest(rolesResult.Errors);


            return new UserDto
            {
                Name = user.Name,
                Token =await _tokenService.CreateToken(user),
                Email = user.Email
            };

        }
    }
}
