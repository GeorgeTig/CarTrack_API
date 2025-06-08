using CarTrack_API.BusinessLogic.Services.UserService;
using CarTrack_API.EntityLayer.Dtos.RefreshToken;
using CarTrack_API.EntityLayer.Dtos.UserDto.LoginDtos;
using CarTrack_API.EntityLayer.Dtos.UserDto.RegisterDtos;
using CarTrack_API.EntityLayer.Exceptions.UserExceptions;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var token = await _userService.LoginAsync(request);
            return Ok(token);
        }
        catch (UserNotFoundException ex)
        {
            return Unauthorized(new { message = ex.Message }); // 401 pentru credențiale greșite
        }
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _userService.RegisterAsync(request);
            return Ok(new { message = "Registration successful." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during registration.", details = ex.Message });
        }
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        if (string.IsNullOrEmpty(request.RefreshToken)) return BadRequest("Refresh token is required.");

        try
        {
            var newTokens = await _userService.RefreshTokenAsync(request.RefreshToken);
            return Ok(newTokens);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}