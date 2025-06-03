using CarTrack_API.BusinessLogic.Services.UserService;
using CarTrack_API.EntityLayer.Dtos.RefreshToken;
using CarTrack_API.EntityLayer.Dtos.UserDto.LoginDtos;
using CarTrack_API.EntityLayer.Dtos.UserDto.RegisterDtos;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController( IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var token = await _userService.LoginAsync(request);
        
        return Ok(token);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto request)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _userService.RegisterAsync(request);
        
        return Ok();
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        if (string.IsNullOrEmpty(request.RefreshToken))
        {
            return BadRequest("Refresh token is required.");
        }

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