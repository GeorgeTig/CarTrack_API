using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.ReminderRepository;

public interface IReminderRepository
{
    // --- Metode de Scriere/Creare ---
    Task AddAsync(Reminder reminder);
    Task UpdateConfigAsync(VehicleMaintenanceConfig config);

    // --- Metode de Citire ---
    Task<List<Reminder>> GetAllByVehicleIdAsync(int vehicleId);
    Task<Reminder?> GetReminderByReminderIdAsync(int reminderId);
    Task<List<Reminder>> GetAllActiveRemindersAsync();
    Task<List<MaintenanceType>> GetAllTypesAsync();
    Task<VehicleMaintenanceConfig?> GetConfigWithVehicleDetailsAsync(int configId);

    // --- Metode de Actualizare ---
    Task UpdateReminderAsync(ReminderRequestDto reminder);
    Task ResetRemindersAsync(int vehicleId, List<int> configIds, double doneMileage, DateTime doneDate);
    Task UpdateReminderActiveAsync(int reminderId);
    
    // --- Metode de Soft Delete ---
    Task<bool> SoftDeleteReminderByConfigIdAsync(int configId);
    Task SoftDeleteAllRemindersForVehicleAsync(int vehicleId);
    
    // --- Metoda de Securitate ---
    Task<bool> DoesUserOwnReminderAsync(int userId, int configId);
    
    // --- Metodă Helper pentru Tranzacții ---
    Task UpdateRemindersAndAddNotificationsAsync(List<Reminder> remindersToUpdate, List<Notification> notificationsToAdd);
}