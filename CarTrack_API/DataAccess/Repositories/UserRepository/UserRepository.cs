using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Exceptions.UserExceptions;
using CarTrack_API.EntityLayer.Exceptions.UserRoleExceptions;
using CarTrack_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.UserRepository;

public class UserRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IUserRepository
{
    
    
    public async Task<User?> GetByEmailAsync(string email)
    {
       var user = await _context.User
                .Include(u => u.Role)
                .Include(u => u.ClientProfile)
                .Include(u => u.ManagerProfile)
                .Include(u => u.MechanicProfile)
                .FirstOrDefaultAsync(u => u.Email == email);

       return user;

    }

    public async Task AddUserAsync(User user)
    {
        if (await GetByEmailAsync(user.Email) != null)
        {
            throw new UserAlreadyExistException($"User with email {user.Email} already exists");
        }
        
        var role = await _context.UserRole.FirstOrDefaultAsync(r => r.Id == user.RoleId);
        
        if (role == null)
        {
            throw new UserRoleNotFoundException("Role not found");
        }
        user.Role = role;
        user.IsActive = true;
        _context.User.Add(user);
        await _context.SaveChangesAsync();
    }
}