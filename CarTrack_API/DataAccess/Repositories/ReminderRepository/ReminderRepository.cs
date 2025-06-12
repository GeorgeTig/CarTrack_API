using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Dtos.VehicleModelDto;
using CarTrack_API.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.ReminderRepository;

public class ReminderRepository : BaseRepository.BaseRepository, IReminderRepository
{
    public ReminderRepository(ApplicationDbContext context) : base(context) { }

    public async Task AddAsync(Reminder reminder)
    {
        _context.Reminder.Add(reminder);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateConfigAsync(VehicleMaintenanceConfig config)
    {
        _context.VehicleMaintenanceConfig.Update(config);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Reminder>> GetAllByVehicleIdAsync(int vehicleId)
    {
        return await _context.Reminder
            .Include(r => r.VehicleMaintenanceConfig.Vehicle)
            .Include(r => r.Status)
            .Include(r => r.VehicleMaintenanceConfig.MaintenanceType)
            .Where(r => r.VehicleMaintenanceConfig.Vehicle.Id == vehicleId && !r.IsDeleted)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Reminder?> GetReminderByReminderIdAsync(int reminderId)
    {
        return await _context.Reminder
            .Include(r => r.VehicleMaintenanceConfig)
            .Include(r => r.Status)
            .Include(r => r.VehicleMaintenanceConfig.MaintenanceType)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.VehicleMaintenanceConfig.Id == reminderId && !r.IsDeleted);
    }

    public async Task<List<Reminder>> GetAllActiveRemindersAsync()
    {
        return await _context.Reminder
            .Include(r => r.VehicleMaintenanceConfig.Vehicle.VehicleInfo)
            .Include(r => r.VehicleMaintenanceConfig.Vehicle.VehicleModel.Producer)
            .Where(r => r.IsActive && !r.IsDeleted && r.VehicleMaintenanceConfig.Vehicle.IsActive)
            .ToListAsync();
    }
    
    public async Task<List<MaintenanceType>> GetAllTypesAsync()
    {
        return await _context.MaintenanceType
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .ToListAsync();
    }
    
    public async Task<VehicleMaintenanceConfig?> GetConfigWithVehicleDetailsAsync(int configId)
    {
        return await _context.VehicleMaintenanceConfig
            .Include(c => c.Vehicle.VehicleInfo)
            .Include(c => c.Vehicle.VehicleModel.VehicleEngine)
            .Include(c => c.Vehicle.VehicleModel.Body)
            .FirstOrDefaultAsync(c => c.Id == configId);
    }
    
    public async Task UpdateReminderAsync(ReminderRequestDto reminderRequest)
    {
        var config = await _context.VehicleMaintenanceConfig.FindAsync(reminderRequest.Id);
        if (config != null)
        {
            config.MileageIntervalConfig = reminderRequest.MileageInterval;
            config.DateIntervalConfig = reminderRequest.TimeInterval;
            await _context.SaveChangesAsync();
        }
    }

    public async Task ResetRemindersAsync(int vehicleId, List<int> configIds, double doneMileage, DateTime doneDate)
    {
        var remindersToReset = await _context.Reminder
            .Include(r => r.VehicleMaintenanceConfig)
            .Where(r => r.VehicleMaintenanceConfig.VehicleId == vehicleId && configIds.Contains(r.VehicleMaintenanceConfigId))
            .ToListAsync();

        foreach (var reminder in remindersToReset)
        {
            var universalDoneDate = doneDate.ToUniversalTime();
            reminder.LastDateCheck = universalDoneDate;
            reminder.LastMileageCkeck = doneMileage;
            reminder.StatusId = 1; // "Up to date"
            
            var config = reminder.VehicleMaintenanceConfig;
            if (config.MileageIntervalConfig != -1) reminder.DueMileage = config.MileageIntervalConfig;
            if (config.DateIntervalConfig != -1) reminder.DueDate = config.DateIntervalConfig;
        }
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateReminderActiveAsync(int reminderId)
    {
        var reminder = await _context.Reminder.FirstOrDefaultAsync(r => r.VehicleMaintenanceConfig.Id == reminderId);
        if (reminder != null)
        {
            reminder.IsActive = !reminder.IsActive;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> SoftDeleteReminderByConfigIdAsync(int configId)
    {
        var reminder = await _context.Reminder.FirstOrDefaultAsync(r => r.VehicleMaintenanceConfigId == configId);
        if (reminder != null)
        {
            reminder.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task SoftDeleteAllRemindersForVehicleAsync(int vehicleId)
    {
        var reminders = await _context.Reminder
            .Include(r => r.VehicleMaintenanceConfig)
            .Where(r => r.VehicleMaintenanceConfig.VehicleId == vehicleId && !r.IsDeleted)
            .ToListAsync();
            
        if (reminders.Any())
        {
            foreach (var reminder in reminders)
            {
                reminder.IsDeleted = true;
            }
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> DoesUserOwnReminderAsync(int userId, int configId)
    {
        return await _context.VehicleMaintenanceConfig
            .AnyAsync(c => c.Id == configId && c.Vehicle.ClientId == userId && c.Vehicle.IsActive);
    }
    
    public async Task UpdateRemindersAndAddNotificationsAsync(List<Reminder> remindersToUpdate, List<Notification> notificationsToAdd)
    {
        if (remindersToUpdate.Any()) _context.Reminder.UpdateRange(remindersToUpdate);
        if (notificationsToAdd.Any()) await _context.Notification.AddRangeAsync(notificationsToAdd);
        if (remindersToUpdate.Any() || notificationsToAdd.Any()) await _context.SaveChangesAsync();
    }
}