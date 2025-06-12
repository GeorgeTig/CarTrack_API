using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.DataAccess.Repositories.ReminderRepository;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.ReminderService;

public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }
        
        public async Task AddReminderAsync(VehicleMaintenanceConfig vehicleMaintenanceConfig, double vehicleMileage)
        {
            var reminder = MappingReminder.ToReminder(vehicleMaintenanceConfig, vehicleMileage);
            await _reminderRepository.AddAsync(reminder);
        }
        
        public async Task<List<ReminderResponseDto>> GetAllRemindersByVehicleIdAsync(int vehicleId)
        {
            var reminders = await _reminderRepository.GetAllByVehicleIdAsync(vehicleId);
            return reminders.ToListReminderResponseDto();
        }
        
        public async Task<ReminderResponseDto> GetReminderByReminderIdAsync(int reminderId)
        {
            var reminder = await _reminderRepository.GetReminderByReminderIdAsync(reminderId);
            return reminder.ToReminderResponseDto();
        }

        public async Task UpdateReminderAsync(ReminderRequestDto reminderRequest)
        {
            await _reminderRepository.UpdateReminderAsync(reminderRequest);
        }
   
        // --- METODA REFACTORIZATĂ ---
        public async Task UpdateReminderAsync(VehicleMaintenanceRequestDto vehicleMaintenanceRequest)
        {
            // Extragem ID-urile configurațiilor care corespund reminderelor efectuate
            var configIdsToReset = vehicleMaintenanceRequest.MaintenanceItems
                .Where(item => item.ConfigId.HasValue)
                .Select(item => item.ConfigId.Value)
                .ToList();
            
            // Dacă nu a fost efectuată nicio lucrare legată de un reminder, ieșim
            if (!configIdsToReset.Any())
            {
                return;
            }

            // Delegăm logica de resetare către repository
            await _reminderRepository.ResetRemindersAsync(
                vehicleMaintenanceRequest.VehicleId,
                configIdsToReset,
                vehicleMaintenanceRequest.Mileage,
                vehicleMaintenanceRequest.Date
            );
        }
    
        public async Task UpdateReminderActiveAsync(int reminderId)
        {
            await _reminderRepository.UpdateReminderActiveAsync(reminderId);
        }
   
        public async Task ProcessReminderUpdatesAsync(double daysPassed)
    {
        var activeReminders = await _reminderRepository.GetAllActiveRemindersAsync();
        
        var remindersToUpdate = new List<Reminder>();
        var notificationsToAdd = new List<Notification>();

        foreach (var reminder in activeReminders)
        {
            if (reminder.VehicleMaintenanceConfig.Vehicle?.VehicleInfo == null) continue;

            bool hasChanged = false;
            var originalStatusId = reminder.StatusId;

            // 1. Actualizăm valorile DueMileage și DueDate
            if (reminder.VehicleMaintenanceConfig.MileageIntervalConfig > 0)
            {
                var mileageToDecrease = reminder.VehicleMaintenanceConfig.Vehicle.VehicleInfo.AverageTravelDistance * daysPassed;
                var newDueMileage = reminder.DueMileage - mileageToDecrease;
                reminder.DueMileage = Math.Max(0, newDueMileage);
                hasChanged = true;
            }

            if (reminder.VehicleMaintenanceConfig.DateIntervalConfig > 0)
            {
                var newDueDate = reminder.DueDate - (int)Math.Round(daysPassed);
                reminder.DueDate = Math.Max(0, newDueDate);
                hasChanged = true;
            }
            
            // 2. Evaluăm noua stare conform pragurilor
            bool isOverdue = (reminder.VehicleMaintenanceConfig.MileageIntervalConfig > 0 && reminder.DueMileage <= 10) || 
                             (reminder.VehicleMaintenanceConfig.DateIntervalConfig > 0 && reminder.DueDate <= 1);
            
            bool isDueSoon = !isOverdue && 
                             ((reminder.VehicleMaintenanceConfig.MileageIntervalConfig > 0 && reminder.DueMileage <= 100) || 
                              (reminder.VehicleMaintenanceConfig.DateIntervalConfig > 0 && reminder.DueDate <= 30));

            if (isOverdue)
            {
                reminder.StatusId = 3; // Overdue
            }
            else if (isDueSoon)
            {
                reminder.StatusId = 2; // Due Soon
            }
            else
            {
                reminder.StatusId = 1; // Up to date
            }

            // 3. Creăm notificare dacă starea s-a înrăutățit
            if (reminder.StatusId > originalStatusId)
            {
                var vehicle = reminder.VehicleMaintenanceConfig.Vehicle;
                var producerName = vehicle.VehicleModel.Producer.Name;
                var seriesName = vehicle.VehicleModel.SeriesName;
                
                string messageType = reminder.StatusId == 2 ? "Upcoming" : "Overdue";
                string message = $"{messageType}: '{reminder.VehicleMaintenanceConfig.Name}' for your {producerName} {seriesName} is due.";

                notificationsToAdd.Add(new Notification
                {
                    VehicleId = vehicle.Id,
                    Message = message,
                    Date = DateTime.UtcNow,
                    IsActive = true,
                    IsRead = false,
                    UserId = vehicle.ClientId,
                    RemiderId = reminder.VehicleMaintenanceConfigId
                });
            }

            // 4. Adăugăm reminder-ul la lista de actualizare dacă s-a schimbat ceva
            if (hasChanged || reminder.StatusId != originalStatusId)
            {
                remindersToUpdate.Add(reminder);
            }
        }

        // 5. Salvăm toate modificările într-o singură tranzacție
        await _reminderRepository.UpdateRemindersAndAddNotificationsAsync(remindersToUpdate, notificationsToAdd);
    }
    }