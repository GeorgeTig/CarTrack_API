using System.Security.Claims;
using CarTrack_API.BusinessLogic.Services.UserService;
using CarTrack_API.EntityLayer.Dtos.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Authorize(Roles = "client")]
[Route("api/user")]
public class AccountController(IUserService userService /*, IEmailService emailService */) : ControllerBase
{
    private readonly IUserService _userService = userService;

    // --- Endpoint pentru a lua informațiile utilizatorului curent (mai sigur decât cu ID în rută) ---
    [HttpGet("profile")]
    public async Task<IActionResult> GetUserProfile()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var user = await _userService.GetUserInfoAsync(userId);

        return Ok(user);
    }

    // --- Endpoint pentru actualizarea profilului (username, telefon) ---
    [HttpPut("update-profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        await _userService.UpdateProfileAsync(userId, request);
        return Ok();
    }

    // --- Endpoint pentru schimbarea parolei ---
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        
        await _userService.ChangePasswordAsync(userId, request);
        return Ok(new { message = "Password changed successfully." });
    }

    
}