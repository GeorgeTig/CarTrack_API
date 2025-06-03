using CarTrack_API.EntityLayer.Dtos.Auth;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.JwtService;

public interface IJwtService
{
    string GenerateJwtToken(User user);
    string GenerateRefreshToken();
    Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
}