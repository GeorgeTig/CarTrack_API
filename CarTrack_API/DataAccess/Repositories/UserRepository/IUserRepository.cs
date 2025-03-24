using CarTrack_API.Models;

namespace CarTrack_API.DataAccess.Repositories.UserRepository;

public interface IUserRepository
{ 
    void Register();
    Task<User?> GetByEmailAsync(string email); 
}