using CarTrack_API.DataAccess.Dtos.RegisterDtos;
using CarTrack_API.Models;

namespace CarTrack_API.BusinessLogic.Services.UserService;

public interface IUserService
{ 
    Task<User?> ValidateUserAsync(string email, string password);
    Task<bool> ValidatePassword(string inputPassword, string storedPassword);
    Task<bool> EmailExistsAsync(string email);
    Task<UserRegisterResponseDto?> RegisterUserAsync(UserRegisterRequestDto registerUser);
    
}