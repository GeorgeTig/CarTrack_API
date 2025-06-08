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
    
    [HttpPost("mark_as_read")] 
    public async Task<IActionResult> MarkNotificationsAsRead([FromBody] NotificationRequestDto requestDto)
    {
        if (!ModelState.IsValid || requestDto?.NotificationIds == null)
        {
            return BadRequest("Invalid request body or missing notification IDs.");
        }

       
        await _notificationService.MarkNotificationsAsReadAsync(requestDto.NotificationIds);
        return Ok(new { message = "Notifications marked as read." }); 
    }
}
