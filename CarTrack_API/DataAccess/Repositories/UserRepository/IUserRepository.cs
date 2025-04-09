using CarTrack_API.Models;

namespace CarTrack_API.DataAccess.Repositories.UserRepository;

public interface IUserRepository
{ 
    Task<User?> GetByEmailAsync(string email); 
    Task AddUserAsync(User user);
}