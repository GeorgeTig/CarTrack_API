using CarTrack_API.DataAccess.DataContext;
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
}