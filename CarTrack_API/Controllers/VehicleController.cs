using System.Security.Claims;
using CarTrack_API.BusinessLogic.Services.ReminderService;
using CarTrack_API.BusinessLogic.Services.VehicleService;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Dtos.Usage;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using CarTrack_API.EntityLayer.Exceptions.VehicleException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Authorize(Roles = "client")]
[Route("api/vehicle")]
public class VehicleController(IVehicleService vehicleService, IReminderService reminderService) : ControllerBase
{
    private readonly IVehicleService _vehicleService = vehicleService;
    private readonly IReminderService _reminderService = reminderService;

    // Obține toate vehiculele pentru utilizatorul autentificat
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var vehicles = await _vehicleService.GetAllByClientIdAsync(userId);
        return Ok(new { result = vehicles });
    }

    // Adaugă un vehicul nou pentru utilizatorul autentificat
    [HttpPost("add")]
    public async Task<IActionResult> AddVehicle([FromBody] VehicleRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        // Forțăm setarea ID-ului de client cu cel din token pentru securitate
        request.ClientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        
        await _vehicleService.AddVehicleAsync(request);
        return Ok(new { message = "Vehicle added successfully." });
    }

    [HttpGet("engine/{vehicleId}")]
    public async Task<IActionResult> GetVehicleEngineByVehicleId([FromRoute] int vehicleId)
    {
        // TODO: Implementează o verificare pentru a te asigura că utilizatorul curent deține acest vehicleId
        var vehicleEngine = await _vehicleService.GetVehicleEngineByVehicleIdAsync(vehicleId);
        return Ok(vehicleEngine);
    }

    [HttpGet("model/{vehicleId}")]
    public async Task<IActionResult> GetVehicleModelByVehicleId([FromRoute] int vehicleId)
    {
        // TODO: Verificare proprietar
        var vehicleModel = await _vehicleService.GetVehicleModelByVehicleIdAsync(vehicleId);
        return Ok(vehicleModel);
    }

    [HttpGet("info/{vehicleId}")]
    public async Task<IActionResult> GetVehicleInfoByVehicleId([FromRoute] int vehicleId)
    {
        // TODO: Verificare proprietar
        var vehicleInfo = await _vehicleService.GetVehicleInfoByVehicleIdAsync(vehicleId);
        return Ok(vehicleInfo);
    }

    [HttpGet("body/{vehicleId}")]
    public async Task<IActionResult> GetVehicleBodyByVehicleId([FromRoute] int vehicleId)
    {
        // TODO: Verificare proprietar
        var vehicleBody = await _vehicleService.GetVehicleBodyByVehicleIdAsync(vehicleId);
        return Ok(vehicleBody);
    }

    [HttpGet("{vehicleId}/reminders")]
    public async Task<IActionResult> GetRemindersByVehicleId([FromRoute] int vehicleId)
    {
        // TODO: Verificare proprietar
        var reminders = await _reminderService.GetAllRemindersByVehicleIdAsync(vehicleId);
        return Ok(reminders);
    }
    
    [HttpGet("reminders/get/{reminderId}")]
    public async Task<IActionResult> GetReminderByReminderId([FromRoute] int reminderId)
    {
        // TODO: Verificare proprietar
        var reminder = await _reminderService.GetReminderByReminderIdAsync(reminderId);
        return Ok(reminder);
    }

    [HttpPost("update/reminder")]
    public async Task<IActionResult> UpdateReminder([FromBody] ReminderRequestDto request)
    {
        // TODO: Verificare proprietar (pe baza request.Id)
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _reminderService.UpdateReminderAsync(request);
        return Ok();
    }
    
    [HttpPost("update/reminder/{reminderId}/active")] 
    public async Task<IActionResult> UpdateReminderActive([FromRoute] int reminderId)
    {
        // TODO: Verificare proprietar
        await _reminderService.UpdateReminderActiveAsync(reminderId);
        return Ok();
    }

    [HttpPost("maintenance")]
    public async Task<IActionResult> AddMaintenance([FromBody] VehicleMaintenanceRequestDto request)
    {
        // TODO: Verificare proprietar (pe baza request.VehicleId)
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _vehicleService.AddVehicleMaintenanceAsync(request);
        return Ok(new { message = "Maintenance log added successfully." });
    }
    
    [HttpGet("{vehicleId}/history/maintenance")]
    public async Task<IActionResult> GetMaintenanceHistory([FromRoute] int vehicleId)
    {
        // TODO: Verificare proprietar
        var history = await _vehicleService.GetMaintenanceHistoryAsync(vehicleId);
        return Ok(history);
    }
    
    [HttpGet("{vehicleId}/usage/daily")]
    public async Task<IActionResult> GetDailyUsageForWeek([FromRoute] int vehicleId, [FromQuery] string timeZoneId)
    {
        // TODO: Verificare proprietar
        if (string.IsNullOrEmpty(timeZoneId)) return BadRequest(new { message = "A valid 'timeZoneId' query parameter is required." });
        
        try
        {
            TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }
        catch (TimeZoneNotFoundException)
        {
            return BadRequest(new { message = $"Invalid time zone ID: {timeZoneId}" });
        }
        
        var dailyUsage = await _vehicleService.GetDailyUsageForLastWeekAsync(vehicleId, timeZoneId);
        return Ok(dailyUsage);
    }
    
    [HttpPost("{vehicleId}/mileage-readings")]
    public async Task<IActionResult> AddMileageReading([FromRoute] int vehicleId, [FromBody] AddMileageReadingRequestDto request)
    {
        // TODO: Verificare proprietar
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        try
        {
            await _vehicleService.AddMileageReadingAsync(vehicleId, request);
            return Ok(new { message = "Mileage updated successfully." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (VehicleNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}