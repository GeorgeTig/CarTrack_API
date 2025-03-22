using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarTrack_API.Models;
using Microsoft.IdentityModel.Tokens;

namespace CarTrack_API.BusinessLogic.Services;

public class JwtTokenService 
{
   private readonly IConfiguration _config;
   
   public JwtTokenService(IConfiguration config)
    {
         _config = config;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Role),
            new Claim("UserId", user.Id.ToString())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}