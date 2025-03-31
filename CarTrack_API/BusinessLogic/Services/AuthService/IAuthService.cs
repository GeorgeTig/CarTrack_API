using CarTrack_API.DataAccess.Dtos.LoginDtos;
using CarTrack_API.DataAccess.Dtos.RegisterDtos;

namespace CarTrack_API.BusinessLogic.Services.AuthService;

public interface IAuthService
{
    Task<UserLoginResponseDto?> RegisterAsync(UserRegisterRequestDto userRegisterDto);
    Task<UserLoginResponseDto?> LoginAsync(string email, string password);
}