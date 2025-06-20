﻿using System.Security.Cryptography;
using System.Text;
using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.BusinessLogic.Services.ClientProfileService;
using CarTrack_API.BusinessLogic.Services.JwtService;
using CarTrack_API.BusinessLogic.Services.ManagerProfileService;
using CarTrack_API.DataAccess.Repositories.RefreshTokenRepository;
using CarTrack_API.DataAccess.Repositories.UserRepository;
using CarTrack_API.EntityLayer.Dtos.Auth;
using CarTrack_API.EntityLayer.Dtos.UserDto;
using CarTrack_API.EntityLayer.Dtos.UserDto.LoginDtos;
using CarTrack_API.EntityLayer.Dtos.UserDto.RegisterDtos;
using CarTrack_API.EntityLayer.Exceptions.UserExceptions;
using CarTrack_API.EntityLayer.Exceptions.UserRoleExceptions;
using CarTrack_API.EntityLayer.Models;


namespace CarTrack_API.BusinessLogic.Services.UserService;

public class UserService(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IJwtService jwtService, IClientProfileService clientProfileService, IManagerProfileService managerProfileService) : IUserService
{
    
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IClientProfileService _clientProfileService = clientProfileService;
    private readonly IManagerProfileService _managerProfileService = managerProfileService;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    


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

   
    public async Task<AuthResponseDto?> LoginAsync(UserLoginRequestDto loginUser)
    {
        var user = await ValidateUserAsync(loginUser.Email, loginUser.Password);
        if (user == null)
        {
            return null;
        }

        var accessToken = _jwtService.GenerateJwtToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        var refreshTokenEntity = MappingRefreshToken.ToRefreshToken(refreshToken,user);

        await _refreshTokenRepository.UpdateRefreshTokenAsync(refreshTokenEntity);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
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

            if (user.RoleId == 1)
            {
                var clientProfile = new ClientProfile
                {
                    UserId = user.Id,
                };

                await _clientProfileService.AddClientProfileAsync(clientProfile);
            }
            else if (user.RoleId == 2)
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
    
    public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
    {
        return await _jwtService.RefreshTokenAsync(refreshToken);
    }
    
    public async Task<UserResponseDto> GetUserInfoAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException($"User with id {userId} not found");
        }

        return MappingUser.ToUserResponseDto(user);
    }
    
    public async Task UpdateProfileAsync(int userId, UpdateProfileRequestDto request)
    {

        var user = await _userRepository.GetByIdAsync(userId);
        
        if (user == null)
        {
            throw new UserNotFoundException($"User with id {userId} not found");
        }
        
        if (string.IsNullOrWhiteSpace(request.Username) && string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            throw new ArgumentException("Update request cannot be empty.");
        }
        
        if (!string.IsNullOrWhiteSpace(request.Username))
        {
            user.Username = request.Username ;
        }
        if (request.PhoneNumber != null && request.PhoneNumber.Length == 10)
        {
            user.PhoneNumber = int.Parse(request.PhoneNumber);
        }

        await _userRepository.UpdateUserAsync(user);
    }
    
    public async Task ChangePasswordAsync(int userId, ChangePasswordRequestDto request)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException($"User with id {userId} not found");
        }

        if (!await ValidatePassword(request.CurrentPassword, user.Password))
        {
            throw new ArgumentException("Incorrect current password.");
        }

        using var sha256 = SHA256.Create();
        var hashedNewPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword)));
        
        user.Password = hashedNewPassword;
        
        await _userRepository.UpdateUserAsync(user);
    }
    
}