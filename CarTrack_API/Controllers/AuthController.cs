using AutoMapper;
using CarTrack_API.BusinessLogic.Services;
using CarTrack_API.BusinessLogic.Services.AuthService;
using CarTrack_API.BusinessLogic.Services.UserService;
using CarTrack_API.DataAccess.Dtos;
using CarTrack_API.DataAccess.Dtos.LoginDtos;
using CarTrack_API.DataAccess.Dtos.RegisterDtos;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtService _service;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    
    public AuthController(JwtService jwtService, IMapper mapper, IAuthService authService)
    {
        _service = jwtService;
        _mapper = mapper;
        _authService = authService;
    }  
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
    {
        var userDto = await _authService.LoginAsync(request.Email, request.Password);
        if (userDto == null)
            return Unauthorized(new { message = "Invalid credentials" });

        return Ok(new { User = userDto });
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto request)
    {
        var userDto = await _authService.RegisterAsync(request);
        if (userDto == null)
            return BadRequest(new { message = "Email already exists" });
        

        return Ok(new { User = userDto });
    }
    
}