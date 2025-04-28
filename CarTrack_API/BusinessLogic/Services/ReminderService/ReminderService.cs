using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.DataAccess.Repositories.ReminderRepository;
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
    
}