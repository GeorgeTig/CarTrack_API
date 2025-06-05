using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Dtos.VehicleModelDto;
using CarTrack_API.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.ReminderRepository;

public class ReminderRepository(ApplicationDbContext context)
    : BaseRepository.BaseRepository(context), IReminderRepository
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

    public async Task UpdateReminderAsync(VehicleMaintenanceRequestDto vehMaintenance)
    {
        
        
        foreach (var item in vehMaintenance.MaintenanceItems)
        {
            var reminder = await _context.Reminder
                .Include(r => r.VehicleMaintenanceConfig)
                .Include(r => r.VehicleMaintenanceConfig.MaintenanceType)
                .Where(r => r.VehicleMaintenanceConfig.MaintenanceTypeId == item.TypeId)
                .Where(r => r.VehicleMaintenanceConfig.Name == item.Name)
                .Where(r => r.VehicleMaintenanceConfig.Vehicle.Id == vehMaintenance.VehicleId)
                .FirstOrDefaultAsync();
            if (reminder != null && reminder.LastMileageCkeck<= vehMaintenance.Mileage &&
                reminder.LastDateCheck <= vehMaintenance.Date)
            {
                reminder.LastDateCheck = vehMaintenance.Date;
                reminder.LastMileageCkeck = vehMaintenance.Mileage;
                reminder.DueMileage = vehMaintenance.Mileage + reminder.VehicleMaintenanceConfig.MileageIntervalConfig;
                reminder.DueDate = reminder.VehicleMaintenanceConfig.DateIntervalConfig;
                reminder.StatusId = 1; // Reset status to OK
                
                _context.Reminder.Update(reminder);
            }
        }
        await _context.SaveChangesAsync();
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

    public async Task ActualizeRemindersDueAsync()
    {
        var reminders = await _context.Reminder
            .Include(r => r.VehicleMaintenanceConfig)
            .Include(r => r.Status)
            .Include(r => r.VehicleMaintenanceConfig.Vehicle)
            .Include(r => r.VehicleMaintenanceConfig.Vehicle.VehicleInfo)
            .ToListAsync();

        foreach (var reminder in reminders)
        {
            if (!reminder.IsActive) continue;

            var vehicle = reminder.VehicleMaintenanceConfig.Vehicle;
            var vehicleInfo = vehicle.VehicleInfo;
            var averageTravel = vehicleInfo.AverageTravelDistance;

            var mileageInterval = reminder.VehicleMaintenanceConfig.MileageIntervalConfig;
            var dateInterval = reminder.VehicleMaintenanceConfig.DateIntervalConfig;

            // Update countdowns
            if (mileageInterval > 0)
            {
                reminder.DueMileage = Math.Max(0, reminder.DueMileage - averageTravel);
                vehicleInfo.Mileage += averageTravel;
            }

            if (dateInterval > 0)
            {
                reminder.DueDate = Math.Max(0, reminder.DueDate - 1);
            }

            // Check conditions
            bool isOverdue = (mileageInterval > 0 && reminder.DueMileage <= 10) ||
                             (dateInterval > 0 && reminder.DueDate <= 0);

            bool isDueSoon = !isOverdue && (
                (mileageInterval > 0 && reminder.DueMileage <= 1000) ||
                (dateInterval > 0 && reminder.DueDate <= 30)
            );

            if (isOverdue && reminder.StatusId != 3)
            {
                reminder.StatusId = 3; // Overdue
                _context.Notification.Add(new Notification
                {
                    VehicleId = vehicle.Id,
                    Message = $"Reminder '{reminder.VehicleMaintenanceConfig.Name}' is overdue!",
                    Date = DateTime.UtcNow,
                    IsActive = true,
                    IsRead = false,
                    UserId = vehicle.ClientId,
                    RemiderId = reminder.VehicleMaintenanceConfigId
                });
            }
            else if (isDueSoon && reminder.StatusId == 1)
            {
                reminder.StatusId = 2; // Due_soon
                _context.Notification.Add(new Notification
                {
                    VehicleId = vehicle.Id,
                    Message = $"Reminder '{reminder.VehicleMaintenanceConfig.Name}' is due soon!",
                    Date = DateTime.UtcNow,
                    IsActive = true,
                    IsRead = false,
                    UserId = vehicle.ClientId,
                    RemiderId = reminder.VehicleMaintenanceConfigId
                });
            }
            else if (!isOverdue && !isDueSoon && reminder.StatusId != 1)
            {
                reminder.StatusId = 1; // OK — no notification needed
            }

            _context.Reminder.Update(reminder);
        }

        await _context.SaveChangesAsync();
    }
}