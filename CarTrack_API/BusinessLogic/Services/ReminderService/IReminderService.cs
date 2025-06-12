using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.ReminderService;


public interface IReminderService
{
    Task AddReminderAsync(VehicleMaintenanceConfig vehicleMaintenanceConfig, double vehicleMileage);
    Task<List<ReminderResponseDto>> GetAllRemindersByVehicleIdAsync(int vehicleId);
    Task<ReminderResponseDto?> GetReminderByReminderIdAsync(int reminderId);
    Task UpdateReminderAsync(ReminderRequestDto reminderRequest);
    Task UpdateReminderAsync(VehicleMaintenanceRequestDto vehicleMaintenanceRequest);
    Task UpdateReminderActiveAsync(int reminderId);
    Task ResetReminderToDefaultAsync(int configId);
    Task SoftDeleteRemindersForVehicleAsync(int vehicleId);
    Task SoftDeleteCustomReminderAsync(int configId);
    Task ProcessReminderUpdatesAsync(double daysPassed);
    Task<bool> UserOwnsReminderAsync(int userId, int configId);
    Task<List<ReminderTypeResponseDto>> GetAllReminderTypesAsync();
}