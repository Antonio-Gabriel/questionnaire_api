using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace QuestionaryApp.Application.Security.Jwt
{
    public abstract class TokenGenerator
    {
        public static string generate(string key, Guid id, string email)
        {
            var secretKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));

            var signinCredentials = new SigningCredentials(
                secretKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", id.ToString()),
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = signinCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}