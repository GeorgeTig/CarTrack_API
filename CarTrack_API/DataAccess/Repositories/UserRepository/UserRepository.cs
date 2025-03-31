using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.UserRepository;

public class UserRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IUserRepository
{
   
    public async Task<bool> ExistUserAsync(string email)
    {
        return await _context.User.AnyAsync(u => u.Email == email);
    }
    
    public async Task<User?> GetByEmailAsync(string email)
    {
       return await _context.User
           .Include(u => u.Role)
           .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddUserAsync(User user)
    {
        _context.User.Add(user);
        await _context.SaveChangesAsync();
    }
}