using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Data.Entities;
using System.Security.Cryptography.X509Certificates;

namespace Models.Utilities
{
    public class Tokenization
    {
        private readonly IConfiguration _configuration;
        public Tokenization(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(User user, string role)
        {
            var listofClaims = new List<Claim>();
            listofClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            listofClaims.Add(new Claim(ClaimTypes.Name, user.UserName, user.Password));
            listofClaims.Add(new Claim(ClaimTypes.Role, role));

            var authSigningKey = Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(listofClaims),
                Expires = DateTime.UtcNow.AddDays(3),


                /*issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,*/
                SigningCredentials =new SigningCredentials(new SymmetricSecurityKey(authSigningKey), SecurityAlgorithms.HmacSha256)

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            var  token= tokenHandler.WriteToken(createdToken);
            return token;
        }
    }
}
