using AutoMapper;
using CarTrack_API.BusinessLogic.Services;
using CarTrack_API.BusinessLogic.Services.UserService;
using CarTrack_API.DataAccess.Dtos;
using CarTrack_API.DataAccess.Dtos.LoginDtos;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    
    public AuthController(JwtTokenService jwtTokenService, IMapper mapper, IUserService userService)
    {
        _tokenService = jwtTokenService;
        _mapper = mapper;
        _userService = userService;
    }   
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userService.ValidateUserAsync(request.Email, request.Password);
        if (user == null)
            return Unauthorized(new { message = "Invalid credentials" });

        var token = _tokenService.GenerateToken(user);
        var userDto = _mapper.Map<UserLoginResponseDto>(user);

        return Ok(new { Token = token, User = userDto });
    }
    
}