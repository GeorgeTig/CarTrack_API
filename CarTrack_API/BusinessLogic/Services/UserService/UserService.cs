using System.Security.Cryptography;
using System.Text;
using CarTrack_API.DataAccess.Dtos.RegisterDtos;
using CarTrack_API.DataAccess.Repositories.UserRepository;
using CarTrack_API.Models;


namespace CarTrack_API.BusinessLogic.Services.UserService;

public class UserService : IUserService
{
    
    private readonly IUserRepository _userRepository;  
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> ValidateUserAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            return null;
        }
        
        if (!ValidatePassword(password, user.Password))
        {
            return null;
        }
        
        return user;
    }
    
    public async Task<UserRegisterResponseDto?> RegisterUserAsync(UserRegisterRequestDto userRequest)
    {
        if( await _userRepository.GetByEmailAsync(userRequest.Email) != null)
        {
            return null;
        }
        
        using var sha256 = SHA256.Create();
        var hashedPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(userRequest.Password)));

        if (await _userRepository.AddUserAsync(userRequest.Username, userRequest.Email, hashedPassword,
                userRequest.Role))
        {
            
        }

        return null;


    }
    
    private bool ValidatePassword(string inputPassword, string storedPassword)
    {
        using var sha256 = SHA256.Create();
        var hashedInput = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword)));
        return hashedInput == storedPassword;
    }
}