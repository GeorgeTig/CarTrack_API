using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.UserRepository;

public class UserRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IUserRepository
{
    public void Register()
    {
        throw new NotImplementedException();
    }
    
    public async Task<User?> GetByEmailAsync(string email)
    {
       return await _context.User
           .Include(u => u.Role)
           .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Boolean> AddUserAsync(string username, string email, string password, string rolename)
    {
        
    }
}