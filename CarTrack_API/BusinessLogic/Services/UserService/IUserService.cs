using CarTrack_API.Models;

namespace CarTrack_API.BusinessLogic.Services.UserService;

public interface IUserService
{ 
    Task<User?> ValidateUserAsync(string email, string password);
    
}