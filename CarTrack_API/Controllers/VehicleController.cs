using CarTrack_API.BusinessLogic.Services.NotificationService;
using CarTrack_API.BusinessLogic.Services.ReminderService;
using CarTrack_API.BusinessLogic.Services.VehicleService;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.Controllers;

[Authorize(Roles = "client")]
[Route("api/vehicle")]
public class VehicleController(
    IVehicleService vehicleService,
    IReminderService reminderService,
    INotificationService notificationService)
    : ControllerBase
{
    private readonly IVehicleService _vehicleService = vehicleService;
    private readonly IReminderService _reminderService = reminderService;
    private readonly INotificationService _notificationService = notificationService;

    [HttpGet("{clientId}")]
    public ActionResult<List<VehicleResponseDto>> GetAll([FromRoute] int clientId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicles = _vehicleService.GetAllByClientIdAsync(clientId);
        return Ok(vehicles);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddVehicle([FromBody] VehicleRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _vehicleService.AddVehicleAsync(request);
        return Ok();
    }

    [HttpGet("engine/{vehId}")]
    public async Task<IActionResult> GetVehicleEngineByVehicleId([FromRoute] int vehId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicleEngine = await _vehicleService.GetVehicleEngineByVehicleIdAsync(vehId);
        return Ok(vehicleEngine);
    }

    [HttpGet("model/{vehId}")]
    public async Task<IActionResult> GetVehicleModelByVehicleId([FromRoute] int vehId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicleModel = await _vehicleService.GetVehicleModelByVehicleIdAsync(vehId);
        return Ok(vehicleModel);
    }

    [HttpGet("info/{vehId}")]
    public async Task<IActionResult> GetVehicleInfoByVehicleId([FromRoute] int vehId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicleInfo = await _vehicleService.GetVehicleInfoByVehicleIdAsync(vehId);
        return Ok(vehicleInfo);
    }

    [HttpGet("usage/{vehId}")]
    public async Task<IActionResult> GetVehicleUsageStatsByVehicleId([FromRoute] int vehId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicleUsageStats = await _vehicleService.GetVehicleUsageStatsByVehicleIdAsync(vehId);
        return Ok(vehicleUsageStats);
    }

    [HttpGet("body/{vehId}")]
    public async Task<IActionResult> GetVehicleBodyByVehicleId([FromRoute] int vehId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicleBody = await _vehicleService.GetVehicleBodyByVehicleIdAsync(vehId);
        return Ok(vehicleBody);
    }

    [HttpGet("reminders/{vehId}")]
    public async Task<IActionResult> GetRemindersByVehicleId([FromRoute] int vehId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var reminders = await _reminderService.GetAllRemindersByVehicleIdAsync(vehId);
        return Ok(reminders);
    }

    [HttpPost("update/reminder")]
    public async Task<IActionResult> UpdateReminder([FromBody] ReminderRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _reminderService.UpdateReminderAsync(request);

        return Ok();
    }

    [HttpPost("update/reminder{reminderId}/default")]
    public async Task<IActionResult> UpdateReminderDefault([FromRoute] int reminderId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }


        return Ok();
    }

    [HttpPost("update/reminder{reminderId}/active")]
    public async Task<IActionResult> UpdateReminderActive([FromRoute] int reminderId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _reminderService.UpdateReminderActiveAsync(reminderId);
        return Ok();
    }

    [HttpGet("{clientId}/notifications")]
    public async Task<IActionResult> GetNotificationsByClientId([FromRoute] int clientId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var notifications = await _notificationService.GetAllNotificationsAsync(clientId);
        return Ok(notifications);
    }

    [HttpPost("maintenance")]
    public async Task<IActionResult> AddMaintenance([FromBody] VehicleMaintenanceRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (request.Date.Kind == DateTimeKind.Unspecified || request.Date.Kind == DateTimeKind.Local)
        {
            request.Date = new DateTime(request.Date.Year, request.Date.Month, request.Date.Day, 0, 0, 0,
                DateTimeKind.Utc);
        }


        await _reminderService.UpdateReminderAsync(request);
        await _vehicleService.AddVehicleMaintenanceAsync(request);

        return Ok(new { message = "Maintenance log added successfully." });
    }
}