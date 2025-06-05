using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.DataAccess.Repositories.ReminderRepository;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.ReminderService;

public class ReminderService(IReminderRepository reminderRepository) : IReminderService
{
    private readonly IReminderRepository _reminderRepository = reminderRepository;
    
    public async Task AddReminderAsync(VehicleMaintenanceConfig vehicleMaintenanceConfig, double vehicleMileage)
    {
        var reminder = MappingReminder.ToReminder(vehicleMaintenanceConfig, vehicleMileage);
        
        await _reminderRepository.AddAsync(reminder);
    }
    
    public async Task<List<ReminderResponseDto>> GetAllRemindersByVehicleIdAsync(int vehicleId)
    {
        var reminders = await _reminderRepository.GetAllByVehicleIdAsync(vehicleId);
        var reminderResponseDtos = reminders.ToListReminderResponseDto();
        
        return reminderResponseDtos;
    }
    
   public async Task UpdateReminderAsync(ReminderRequestDto reminderRequest)
    {
        await _reminderRepository.UpdateReminderAsync(reminderRequest);
    }
   
    public async Task UpdateReminderAsync(VehicleMaintenanceRequestDto vehicleMaintenanceRequest)
    {
        
        await _reminderRepository.UpdateReminderAsync(vehicleMaintenanceRequest);
    }
    
   public async Task UpdateReminderActiveAsync(int reminderId)
    {
        await _reminderRepository.UpdateReminderActiveAsync(reminderId);
    }
   
    public async Task ActualizeRemindersDueAsync()
    {
        await _reminderRepository.ActualizeRemindersDueAsync();
    }
}