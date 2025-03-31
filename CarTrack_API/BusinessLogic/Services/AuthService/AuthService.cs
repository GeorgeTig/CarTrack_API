using AutoMapper;
using CarTrack_API.BusinessLogic.Services.UserRoleService;
using CarTrack_API.BusinessLogic.Services.UserService;
using CarTrack_API.DataAccess.Dtos.LoginDtos;
using CarTrack_API.DataAccess.Dtos.RegisterDtos;


namespace CarTrack_API.BusinessLogic.Services.AuthService;

public class AuthService
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;
    
    public AuthService(IJwtService jwtService, IUserService userService, IMapper mapper, IUserRoleService userRoleService)
    {
        _userService = userService;
        _mapper = mapper;
        _jwtService = jwtService;
    }   
    
    public async Task<UserLoginResponseDto?> LoginAsync(UserLoginRequestDto requestUser)
    {
        var user = await _userService.ValidateUserAsync(requestUser.Email, requestUser.Password);
        if (user == null)
        {
            return null;
        }
        
        string token = _jwtService.GenerateJwtToken(user);
        
        var response = _mapper.Map<UserLoginResponseDto>(user);
        response.Token = _jwtService.GenerateJwtToken(user);
        return _mapper.Map<UserLoginResponseDto>(user);
    }
    
    public async Task<UserRegisterResponseDto?> RegisterUserAsync(UserRegisterRequestDto userRequest)
    {

        return await _userService.RegisterUserAsync(userRequest);
        
    }
}