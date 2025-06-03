using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.RefreshTokenRepository;

public class RefreshTokenRepository(ApplicationDbContext context)
    : BaseRepository.BaseRepository(context), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshToken
            .Include(rt => rt.User)
            .Include(rt => rt.User.Role)
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task AddRefreshTokenAsync(RefreshToken token)
    {
        await _context.RefreshToken.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRefreshTokenAsync(RefreshToken token)
    {
        var existingToken = await _context.RefreshToken
            .FirstOrDefaultAsync(rt => rt.UserId == token.UserId);

        if (existingToken != null)
            _context.RefreshToken.Update(token);
        else
            await _context.RefreshToken.AddAsync(token);
        
        await _context.SaveChangesAsync();
    }
}