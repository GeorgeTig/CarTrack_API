using CarTrack_API.BusinessLogic.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Authorize(Roles = "client")]
[Route("api/user")]
public class UserController (IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    
    [HttpGet("{userId}info")]
    public async Task<ActionResult> GetUserInfo([FromRoute] int userId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userService.GetUserInfoAsync(userId);
        return Ok(user);
    }
   
}