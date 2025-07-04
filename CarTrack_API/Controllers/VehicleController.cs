﻿using System.Security.Claims;
using CarTrack_API.BusinessLogic.Services.ReminderService;
using CarTrack_API.BusinessLogic.Services.VehicleService;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Dtos.Usage;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using CarTrack_API.EntityLayer.Exceptions.VehicleException;
using CarTrack_API.EntityLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Authorize(Roles = "client")]
[Route("api/vehicle")]
public class VehicleController(IVehicleService vehicleService, IReminderService reminderService) : ControllerBase
{
    private readonly IVehicleService _vehicleService = vehicleService;
    private readonly IReminderService _reminderService = reminderService;

    private int GetCurrentUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetCurrentUserId();
        var vehicles = await _vehicleService.GetAllByClientIdAsync(userId);
        return Ok(new { result = vehicles });
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddVehicle([FromBody] VehicleRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        request.ClientId = GetCurrentUserId();
        
        await _vehicleService.AddVehicleAsync(request);
        return Ok(new { message = "Vehicle added successfully." });
    }


    [HttpGet("engine/{vehicleId}")]
    public async Task<IActionResult> GetVehicleEngineByVehicleId([FromRoute] int vehicleId)
    {
        if (!await _vehicleService.UserOwnsVehicleAsync(GetCurrentUserId(), vehicleId))
            return Forbid(); 
        
        var vehicleEngine = await _vehicleService.GetVehicleEngineByVehicleIdAsync(vehicleId);
        return Ok(vehicleEngine);
    }

    [HttpGet("model/{vehicleId}")]
    public async Task<IActionResult> GetVehicleModelByVehicleId([FromRoute] int vehicleId)
    {
        if (!await _vehicleService.UserOwnsVehicleAsync(GetCurrentUserId(), vehicleId))
            return Forbid();
        
        var vehicleModel = await _vehicleService.GetVehicleModelByVehicleIdAsync(vehicleId);
        return Ok(vehicleModel);
    }

    [HttpGet("info/{vehicleId}")]
    public async Task<IActionResult> GetVehicleInfoByVehicleId([FromRoute] int vehicleId)
    {
        if (!await _vehicleService.UserOwnsVehicleAsync(GetCurrentUserId(), vehicleId))
            return Forbid();
        
        var vehicleInfo = await _vehicleService.GetVehicleInfoByVehicleIdAsync(vehicleId);
        return Ok(vehicleInfo);
    }

    [HttpGet("body/{vehicleId}")]
    public async Task<IActionResult> GetVehicleBodyByVehicleId([FromRoute] int vehicleId)
    {
        if (!await _vehicleService.UserOwnsVehicleAsync(GetCurrentUserId(), vehicleId))
            return Forbid();
    
        var vehicleBody = await _vehicleService.GetBodyByVehicleIdAsync(vehicleId);

        if (vehicleBody == null)
        {
            return NotFound(new { message = $"Body information not found for vehicle with ID {vehicleId}." });
        }
        
        return Ok(vehicleBody);
    }

    [HttpGet("{vehicleId}/reminders")]
    public async Task<IActionResult> GetRemindersByVehicleId([FromRoute] int vehicleId)
    {
        if (!await _vehicleService.UserOwnsVehicleAsync(GetCurrentUserId(), vehicleId))
            return Forbid();
        
        var reminders = await _reminderService.GetAllRemindersByVehicleIdAsync(vehicleId);
        return Ok(reminders);
    }
    
    [HttpPost("maintenance")]
    public async Task<IActionResult> AddMaintenance([FromBody] VehicleMaintenanceRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await _vehicleService.UserOwnsVehicleAsync(GetCurrentUserId(), request.VehicleId))
            return Forbid();
        
        await _vehicleService.AddVehicleMaintenanceAsync(request);
        return Ok(new { message = "Maintenance log added successfully." });
    }
    
    [HttpGet("{vehicleId}/history/maintenance")]
    public async Task<IActionResult> GetMaintenanceHistory([FromRoute] int vehicleId)
    {
        if (!await _vehicleService.UserOwnsVehicleAsync(GetCurrentUserId(), vehicleId))
            return Forbid();
        
        var history = await _vehicleService.GetMaintenanceHistoryAsync(vehicleId);
        return Ok(history);
    }
    
    [HttpGet("{vehicleId}/usage/daily")]
    public async Task<IActionResult> GetDailyUsageForWeek([FromRoute] int vehicleId, [FromQuery] string timeZoneId)
    {
        if (!await _vehicleService.UserOwnsVehicleAsync(GetCurrentUserId(), vehicleId))
            return Forbid();
        
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
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await _vehicleService.UserOwnsVehicleAsync(GetCurrentUserId(), vehicleId))
            return Forbid();
        
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
    
    [HttpGet("reminders/get/{reminderId}")]
    public async Task<IActionResult> GetReminderByReminderId([FromRoute] int reminderId)
    {
        if (!await _reminderService.UserOwnsReminderAsync(GetCurrentUserId(), reminderId))
            return Forbid();
        
        var reminder = await _reminderService.GetReminderByReminderIdAsync(reminderId);
        return Ok(reminder);
    }

    [HttpPost("update/reminder")]
    public async Task<IActionResult> UpdateReminder([FromBody] ReminderRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await _reminderService.UserOwnsReminderAsync(GetCurrentUserId(), request.Id))
            return Forbid();
        
        await _reminderService.UpdateReminderAsync(request);
        return Ok();
    }
    
    [HttpPost("update/reminder/{reminderId}/active")] 
    public async Task<IActionResult> UpdateReminderActive([FromRoute] int reminderId)
    {
        if (!await _reminderService.UserOwnsReminderAsync(GetCurrentUserId(), reminderId))
            return Forbid();
        
        await _reminderService.UpdateReminderActiveAsync(reminderId);
        return Ok();
    }
    
    [HttpDelete("{vehicleId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeactivateVehicle([FromRoute] int vehicleId)
    {
        if (!await _vehicleService.UserOwnsVehicleAsync(GetCurrentUserId(), vehicleId))
        {
            return Forbid(); 
        }

        await _vehicleService.DeactivateVehicleAsync(vehicleId);
        
        return NoContent();
    }

    
    [HttpPost("{vehicleId}/reminders/add-custom")]
    [ProducesResponseType(typeof(VehicleMaintenanceConfig), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddCustomReminder(
        [FromRoute] int vehicleId, 
        [FromBody] CustomReminderRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await _vehicleService.UserOwnsVehicleAsync(GetCurrentUserId(), vehicleId))
        {
            return Forbid();
        }

        try
        {
            await _vehicleService.AddCustomReminderAsync(vehicleId, request);
            return StatusCode(201, new { message = "Custom reminder added successfully." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpGet("reminders/types")]
    [ProducesResponseType(typeof(List<ReminderTypeResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllReminderTypes()
    {
        var types = await _reminderService.GetAllReminderTypesAsync();
        return Ok(types);
    }
    
    [HttpDelete("reminders/{configId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeactivateCustomReminder([FromRoute] int configId)
    {
        if (!await _reminderService.UserOwnsReminderAsync(GetCurrentUserId(), configId))
        {
            return NotFound();
        }
        try
        {
            await _reminderService.SoftDeleteCustomReminderAsync(configId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("reminders/{configId}/reset-to-default")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResetReminderToDefault([FromRoute] int configId)
    {
        if (!await _reminderService.UserOwnsReminderAsync(GetCurrentUserId(), configId))
        {
            return Forbid();
        }

        try
        {
            await _reminderService.ResetReminderToDefaultAsync(configId);
            return NoContent(); 
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}