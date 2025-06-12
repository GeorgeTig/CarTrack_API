using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.ReminderRepository;

public interface IReminderRepository
{
    Task AddAsync(Reminder reminder);
    Task<List<Reminder>> GetAllByVehicleIdAsync(int vehicleId);
    Task<Reminder> GetReminderByReminderIdAsync(int reminderId);
    Task UpdateReminderAsync(ReminderRequestDto reminder);
    Task ResetRemindersAsync(int vehicleId, List<int> configIds, double doneMileage, DateTime doneDate);
    Task UpdateReminderActiveAsync(int reminderId);

    // --- METODE NOI/MODIFICATE ---
    Task<List<Reminder>> GetAllActiveRemindersAsync();
    Task UpdateRemindersAndAddNotificationsAsync(List<Reminder> remindersToUpdate, List<Notification> notificationsToAdd);
}
