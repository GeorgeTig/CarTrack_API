using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.ReminderService;

public interface IReminderService
{
    Task AddReminderAsync(VehicleMaintenanceConfig vehicleMaintenanceConfig, double vehicleMileage);
    Task<List<ReminderResponseDto>> GetAllRemindersByVehicleIdAsync(int vehicleId);
    Task UpdateReminderAsync(ReminderRequestDto reminderRequest);
    Task UpdateReminderAsync(VehicleMaintenanceRequestDto vehicleMaintenanceRequest);
    Task UpdateReminderActiveAsync(int reminderId);
    Task ActualizeRemindersDueAsync();
    public Task<ReminderResponseDto> GetReminderByReminderIdAsync(int reminderId);
}