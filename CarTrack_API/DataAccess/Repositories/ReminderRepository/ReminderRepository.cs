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

        public async Task<List<Reminder>> GetAllByVehicleIdAsync(int vehicleId)
        {
            return await _context.Reminder
                .Include(r => r.VehicleMaintenanceConfig)
                    .ThenInclude(vc => vc.Vehicle)
                .Include(r => r.Status)
                .Include(r => r.VehicleMaintenanceConfig.MaintenanceType)
                .Where(r => r.VehicleMaintenanceConfig.Vehicle.Id == vehicleId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Reminder> GetReminderByReminderIdAsync(int reminderId)
        {
            var reminder = await _context.Reminder
                .Include(r => r.VehicleMaintenanceConfig)
                .Include(r => r.Status)
                .Include(r => r.VehicleMaintenanceConfig.MaintenanceType)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.VehicleMaintenanceConfig.Id == reminderId);

            if (reminder == null)
            {
                throw new KeyNotFoundException($"Reminder with Config ID {reminderId} not found.");
            }
            return reminder;
        }

        public async Task UpdateReminderAsync(ReminderRequestDto reminderRequest)
        {
            var config = await _context.VehicleMaintenanceConfig
                .FirstOrDefaultAsync(vc => vc.Id == reminderRequest.Id);

            if (config != null)
            {
                config.MileageIntervalConfig = reminderRequest.MileageInterval;
                config.DateIntervalConfig = reminderRequest.TimeInterval;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Maintenance Config with ID {reminderRequest.Id} not found.");
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
                var config = reminder.VehicleMaintenanceConfig;
                var universalDoneDate = doneDate.ToUniversalTime();
                
                if (config.MileageIntervalConfig != -1)
                {
                    reminder.LastMileageCkeck = doneMileage;
                    reminder.DueMileage = config.MileageIntervalConfig;
                }
                if (config.DateIntervalConfig != -1)
                {
                    reminder.LastDateCheck = universalDoneDate;
                    reminder.DueDate = config.DateIntervalConfig;
                }
                reminder.StatusId = 1;

                _context.Reminder.Update(reminder);
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
                throw new KeyNotFoundException($"Reminder with Config ID {reminderId} not found.");
            }
        }

        public async Task<List<Reminder>> GetAllActiveRemindersAsync()
        {
            return await _context.Reminder
                .Include(r => r.VehicleMaintenanceConfig)
                .ThenInclude(vc => vc.Vehicle)
                .ThenInclude(v => v.VehicleInfo)
                .Include(r => r.VehicleMaintenanceConfig.Vehicle.VehicleModel.Producer)
                .Where(r => r.IsActive)
                .ToListAsync();
        }

        public async Task UpdateRemindersAndAddNotificationsAsync(List<Reminder> remindersToUpdate, List<Notification> notificationsToAdd)
        {
            if (remindersToUpdate.Any())
            {
                _context.Reminder.UpdateRange(remindersToUpdate);
            }

            if (notificationsToAdd.Any())
            {
                await _context.Notification.AddRangeAsync(notificationsToAdd);
            }

            if (remindersToUpdate.Any() || notificationsToAdd.Any())
            {
                await _context.SaveChangesAsync();
            }
        }
    }