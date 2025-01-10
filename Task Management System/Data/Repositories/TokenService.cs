using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyTask_Management_System.Data.Repositories
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;

            // Get the key from configuration and ensure it's 64 bytes (512 bits)
            string secretKey = _configuration["Token:Key"];

            if (secretKey.Length < 64) // Check if the key is smaller than 512 bits
            {
                throw new ArgumentException("Key size must be at least 512 bits (64 bytes).");
            }

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.DisplayName),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = creds,
                Issuer = _configuration["Token:Issuer"],
                // Uncomment if you are using audience
                // Audience = _configuration["Token:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
