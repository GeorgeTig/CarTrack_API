using System.Security.Claims;
using CarTrack_API.BusinessLogic.Services.NotificationService;
using CarTrack_API.EntityLayer.Dtos.NotificationDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Authorize(Roles = "client")]
[Route("api/notification")]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    private readonly INotificationService _notificationService = notificationService;

    [HttpGet("all")]
    public async Task<IActionResult> GetAllNotifications()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var notifications = await _notificationService.GetAllNotificationsAsync(userId);
        return Ok(notifications);
    }
    
    [HttpPost("mark-as-read")] 
    public async Task<IActionResult> MarkNotificationsAsRead([FromBody] NotificationRequestDto requestDto)
    {
        if (!ModelState.IsValid || requestDto?.NotificationIds == null || !requestDto.NotificationIds.Any())
        {
            return BadRequest("A non-empty list of notification IDs is required.");
        }
        await _notificationService.MarkNotificationsAsReadAsync(requestDto.NotificationIds);
        return Ok(new { message = "Notifications marked as read." }); 
    }
}