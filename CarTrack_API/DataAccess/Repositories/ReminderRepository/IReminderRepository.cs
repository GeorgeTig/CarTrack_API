using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.ReminderRepository;

public interface IReminderRepository
{
    Task AddAsync(Reminder reminder);
    Task<List<Reminder>> GetAllByVehicleIdAsync(int vehicleId);
    Task UpdateReminderAsync(ReminderRequestDto reminder);
    Task UpdateReminderAsync(VehicleMaintenanceRequestDto vehicleMaintenanceRequest);
    Task UpdateReminderActiveAsync(int reminderId);
    Task ActualizeRemindersDueAsync();
    Task<Reminder> GetReminderByReminderIdAsync(int reminderId);
}