using CarTrack_API.Models;

namespace CarTrack_API.BusinessLogic.Services;

public interface IJwtService
{
    string GenerateJwtToken(User user);
}