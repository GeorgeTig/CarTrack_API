using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarTrack_API.Models;
using Microsoft.IdentityModel.Tokens;

namespace CarTrack_API.BusinessLogic.Services;

public class JwtService : IJwtService
{
   private readonly IConfiguration _config;
   
   public JwtService(IConfiguration config)
    {
         _config = config;
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),  
            new Claim(JwtRegisteredClaimNames.Email, user.Email),        
            new Claim("username", user.Username),                        
            new Claim("profileType", user.ClientProfile != null ? "Client" : user.ManagerProfile != null ? "Manager" : "Mechanic"), 
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // Unique token ID
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer:_config["Jwt:Issuer"],
            audience:_config["Jwt:Audience"],
            claims:claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}