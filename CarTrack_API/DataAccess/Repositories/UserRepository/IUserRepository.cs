using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.UserRepository;

public interface IUserRepository
{ 
    Task<User?> GetByEmailAsync(string email); 
    Task<User> GetByIdAsync(int id); 
    Task AddUserAsync(User user);
}