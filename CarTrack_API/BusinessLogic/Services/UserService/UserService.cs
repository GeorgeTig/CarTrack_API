using System.Security.Cryptography;
using System.Text;
using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.BusinessLogic.Services.ClientProfileService;
using CarTrack_API.BusinessLogic.Services.JwtService;
using CarTrack_API.BusinessLogic.Services.ManagerProfileService;
using CarTrack_API.DataAccess.Repositories.UserRepository;
using CarTrack_API.EntityLayer.Dtos.UserDto.LoginDtos;
using CarTrack_API.EntityLayer.Dtos.UserDto.RegisterDtos;
using CarTrack_API.EntityLayer.Exceptions.UserExceptions;
using CarTrack_API.EntityLayer.Exceptions.UserRoleExceptions;
using CarTrack_API.EntityLayer.Models;


namespace CarTrack_API.BusinessLogic.Services.UserService;

public class UserService(IUserRepository userRepository, IJwtService jwtService, IClientProfileService clientProfileService, IManagerProfileService managerProfileService) : IUserService
{
    
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IClientProfileService _clientProfileService = clientProfileService;
    private readonly IManagerProfileService _managerProfileService = managerProfileService;
    


    public async Task<User?> ValidateUserAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            return null;
        }
        
        if (!await ValidatePassword(password, user.Password))
        {
            return null;
        }
        
        return user;
    }
    
    
    
    public async Task<bool> ValidatePassword(string inputPassword, string storedPassword)
    {
        using var sha256 = SHA256.Create();
        var hashedInput = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword)));
        return hashedInput == storedPassword;
    }

   
    public async Task<string?> LoginAsync(UserLoginRequestDto loginUser)
    {
        var user = await ValidateUserAsync(loginUser.Email, loginUser.Password);
        if (user == null)
        {
            return null;
        }
        
        var token = _jwtService.GenerateJwtToken(user);

        return token;
    }
    
    public async Task RegisterAsync(UserRegisterRequestDto registerUser)
    {

        try
        {
            using var sha256 = SHA256.Create();
            var hashedPassword =
                Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password)));
            registerUser.Password = hashedPassword;
            var user = registerUser.ToUser();
            await _userRepository.AddUserAsync(user);

            if (user.Id == 1)
            {
                var clientProfile = new ClientProfile
                {
                    UserId = user.Id,
                };

                await _clientProfileService.AddClientProfileAsync(clientProfile);
            }
            else if (user.Id == 2)
            {
                var managerProfile = new ManagerProfile
                {
                    UserId = user.Id,
                };

                await _managerProfileService.AddManagerProfileAsync(managerProfile);
            }
        }
        catch (UserAlreadyExistException)
        {
            throw;
        }
        catch (UserRoleNotFoundException)
        {
            throw;
        }
        
    }
    
}