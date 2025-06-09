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
                reminder.LastDateCheck = doneDate.ToUniversalTime();
                reminder.LastMileageCkeck = doneMileage;
                reminder.DueMileage = reminder.VehicleMaintenanceConfig.MileageIntervalConfig;
                reminder.DueDate = reminder.VehicleMaintenanceConfig.DateIntervalConfig;
                reminder.StatusId = 1; // Reset status to "Up to date"

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

        public async Task ActualizeRemindersDueAsync()
        {
            var reminders = await _context.Reminder
                .Include(r => r.VehicleMaintenanceConfig)
                    .ThenInclude(vc => vc.Vehicle)
                        .ThenInclude(v => v.VehicleInfo)
                .Where(r => r.IsActive)
                .ToListAsync();
            
            var notificationsToAdd = new List<Notification>();

            foreach (var reminder in reminders)
            {
                // Verificare de siguranță
                if (reminder.VehicleMaintenanceConfig.Vehicle?.VehicleInfo == null)
                {
                    continue;
                }

                var vehicle = reminder.VehicleMaintenanceConfig.Vehicle;
                var vehicleInfo = vehicle.VehicleInfo;
                var averageTravel = vehicleInfo.AverageTravelDistance;

                var mileageInterval = reminder.VehicleMaintenanceConfig.MileageIntervalConfig;
                var dateInterval = reminder.VehicleMaintenanceConfig.DateIntervalConfig;

                // --- Actualizează contoarele ---
                if (mileageInterval > 0)
                {
                    // Scade din kilometrajul rămas distanța medie parcursă
                    reminder.DueMileage = Math.Max(0, reminder.DueMileage - averageTravel);
                }

                if (dateInterval > 0)
                {
                    // Scade o zi din timpul rămas
                    reminder.DueDate = Math.Max(0, reminder.DueDate - 1);
                }

                // --- Evaluează starea reminder-ului ---
                // Stabilește praguri pentru "Due Soon"
                const int dueSoonMileageThreshold = 1000; // 1000 km
                const int dueSoonTimeThreshold = 30; // 30 de zile

                bool isOverdue = (mileageInterval > 0 && reminder.DueMileage <= 0) || (dateInterval > 0 && reminder.DueDate <= 0);
                
                bool isDueSoon = !isOverdue && 
                                 ((mileageInterval > 0 && reminder.DueMileage <= dueSoonMileageThreshold) || 
                                  (dateInterval > 0 && reminder.DueDate <= dueSoonTimeThreshold));

                // --- Schimbă starea și creează notificări doar dacă starea se schimbă ---
                if (isOverdue && reminder.StatusId != 3) // 3 = Overdue
                {
                    reminder.StatusId = 3;
                    notificationsToAdd.Add(new Notification
                    {
                        VehicleId = vehicle.Id,
                        Message = $"Overdue: '{reminder.VehicleMaintenanceConfig.Name}' for your {vehicle.VehicleModel.Producer.Name} {vehicle.VehicleModel.SeriesName} is past due.",
                        Date = DateTime.UtcNow,
                        IsActive = true,
                        IsRead = false,
                        UserId = vehicle.ClientId,
                        RemiderId = reminder.VehicleMaintenanceConfigId
                    });
                }
                else if (isDueSoon && reminder.StatusId == 1) // 1 = Up to date
                {
                    reminder.StatusId = 2; // 2 = Due Soon
                    notificationsToAdd.Add(new Notification
                    {
                        VehicleId = vehicle.Id,
                        Message = $"Upcoming: '{reminder.VehicleMaintenanceConfig.Name}' for your {vehicle.VehicleModel.Producer.Name} {vehicle.VehicleModel.SeriesName} is due soon.",
                        Date = DateTime.UtcNow,
                        IsActive = true,
                        IsRead = false,
                        UserId = vehicle.ClientId,
                        RemiderId = reminder.VehicleMaintenanceConfigId
                    });
                }
                // Nu resetăm la "Up to date" aici. Resetarea se face doar când se adaugă o mentenanță.
            }

            // Adaugă toate notificările create într-un singur batch
            if (notificationsToAdd.Any())
            {
                await _context.Notification.AddRangeAsync(notificationsToAdd);
            }
            
            // Salvează toate modificările (starea reminderelor și noile notificări) la final
            await _context.SaveChangesAsync();
        }
    }