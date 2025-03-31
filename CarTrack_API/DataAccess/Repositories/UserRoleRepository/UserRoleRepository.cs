using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.UserRoleRepository;

public class UserRoleRepository(ApplicationDbContext context ) : BaseRepository.BaseRepository(context), IUserRoleRepository
{
    public async Task<UserRole?> GetUserRoleAsync(string roleName)
    {
        return await _context.UserRole.FirstOrDefaultAsync(r => r.Role == roleName);
    }
}