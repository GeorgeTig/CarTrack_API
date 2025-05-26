using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.ReminderRepository;

public class ReminderRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IReminderRepository 
{
    public async Task AddAsync(Reminder reminder)
    {
        _context.Reminder.Add(reminder);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<Reminder>> GetAllByVehicleIdAsync(int vehicleId)
    {
        var reminders = await _context.Reminder
            .Include(r => r.VehicleMaintenanceConfig)
            .Include(r => r.VehicleMaintenanceConfig.Vehicle)
            .Include(r => r.Status)
            .Include(r => r.VehicleMaintenanceConfig.MaintenanceType)
            .Include(r => r.VehicleMaintenanceConfig.MaintenanceType)
            .Where(r => r.VehicleMaintenanceConfig.Vehicle.Id == vehicleId)
            .ToListAsync();
        
        return reminders;
    }
    
    public async Task UpdateReminderAsync(ReminderRequestDto reminderRequest)
    {
        var reminder = await _context.Reminder
            .Include(r => r.VehicleMaintenanceConfig)
            .FirstOrDefaultAsync(r => r.VehicleMaintenanceConfig.Id == reminderRequest.Id);
        
        if (reminder != null)
        {
            reminder.VehicleMaintenanceConfig.MileageIntervalConfig = reminderRequest.MileageInterval;
            reminder.VehicleMaintenanceConfig.DateIntervalConfig = reminderRequest.TimeInterval;
            _context.Reminder.Update(reminder);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException($"Reminder with ID {reminderRequest.Id} not found.");
        }
    }
    
    public async Task UpdateReminderActiveAsync(int reminderId)
    {
        var reminder = await _context.Reminder
            .FirstOrDefaultAsync(r => r.VehicleMaintenanceConfig.Id == reminderId);
        
        if (reminder != null)
        {
            reminder.IsActive = !reminder.IsActive;
            _context.Reminder.Update(reminder);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException($"Reminder with ID {reminderId} not found.");
        }
    }
}