using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.RefreshTokenRepository;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task AddRefreshTokenAsync(RefreshToken token);
    Task UpdateRefreshTokenAsync(RefreshToken token);
}