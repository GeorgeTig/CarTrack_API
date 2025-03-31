using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using CarTrack_API.BusinessLogic.Services.UserRoleService;
using CarTrack_API.DataAccess.Dtos.RegisterDtos;
using CarTrack_API.DataAccess.Repositories.UserRepository;
using CarTrack_API.DataAccess.Repositories.UserRoleRepository;
using CarTrack_API.Models;


namespace CarTrack_API.BusinessLogic.Services.UserService;

public class UserService : IUserService
{
    
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleService _userRoleService;
    private readonly IMapper _mapper;
    
    public UserService(IUserRepository userRepository, IMapper mapper, IUserRoleService userRoleService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userRoleService = userRoleService;
    }

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
    
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email) != null;
    }
    
    public async Task<UserRegisterResponseDto?> RegisterUserAsync(UserRegisterRequestDto registerUser)
    {
        if (await EmailExistsAsync(registerUser.Email))
        {
            return null;
        }
        
        using var sha256 = SHA256.Create();
        var hashedPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password)));
        registerUser.Password = hashedPassword;
        
        var user = _mapper.Map<User>(registerUser);
        if (user.Role.Role.ToLower() == "client")
        {
            user.ClientProfile = new ClientProfile
            {
                User = user,
                UserId = user.Id
            };
        }
        else if (user.Role.Role.ToLower() == "manager")
        {
            user.ManagerProfile = new ManagerProfile
            {
                User = user,
                UserId = user.Id
            };
        }
        else
        {
            user.MechanicProfile = new MechanicProfile
            {
                User = user,
                UserId = user.Id
            };
        }

        await _userRepository.AddUserAsync(user);
        
        return _mapper.Map<UserRegisterResponseDto>(user);
    }
    
}