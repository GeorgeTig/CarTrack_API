using CarTrack_API.EntityLayer.Dtos.UserDto.LoginDtos;
using CarTrack_API.EntityLayer.Dtos.UserDto.RegisterDtos;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.UserService;

public interface IUserService
{ 
    Task<User?> ValidateUserAsync(string email, string password);
    Task<bool> ValidatePassword(string inputPassword, string storedPassword);
    Task RegisterAsync(UserRegisterRequestDto registerUser);
    Task<string?> LoginAsync(UserLoginRequestDto loginUser);   
    
}