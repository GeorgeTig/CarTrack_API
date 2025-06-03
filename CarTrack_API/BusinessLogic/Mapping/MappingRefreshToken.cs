using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingRefreshToken
{
    public static RefreshToken ToRefreshToken(string token, User user)
    {
        return new RefreshToken
        {
            Token = token,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            IsRevoked = false,
            UserId = user.Id,
            User = user
        };
    }   
    
}