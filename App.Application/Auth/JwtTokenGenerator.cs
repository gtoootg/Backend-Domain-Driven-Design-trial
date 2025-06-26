using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace App.Application.Auth;

public class JwtTokenGenerator
{
    public static string GenerateToken(string userId, string email, string secretKey)
    {
     var claims = new []
     {
         new Claim(JwtRegisteredClaimNames.Sub, userId),
         new Claim(JwtRegisteredClaimNames.Email, email),
         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
     };
     
     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
     var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
     
     var token = new JwtSecurityToken(
         claims: claims,
         expires: DateTime.Now.AddMinutes(1),
         signingCredentials: credentials
         );
     
     return  new JwtSecurityTokenHandler().WriteToken(token);
    }
}