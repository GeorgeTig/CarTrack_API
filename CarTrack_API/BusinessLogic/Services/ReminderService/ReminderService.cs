using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.BusinessLogic.Services.MaintenanceCalculatorService;
using CarTrack_API.DataAccess.Repositories.ReminderRepository;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.ReminderService;

public class ReminderService : IReminderService
{
    private readonly IReminderRepository _reminderRepository;
    private readonly IMaintenanceCalculatorService _calculatorService; // <-- DEPENDENȚĂ NOUĂ


    public ReminderService(IReminderRepository reminderRepository, IMaintenanceCalculatorService calculatorService)
    {
        _reminderRepository = reminderRepository;
        _calculatorService = calculatorService; 

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
    
    public async Task DeactivateRemindersForVehicleAsync(int vehicleId)
    {
        await _reminderRepository.DeactivateAllRemindersForVehicleAsync(vehicleId);
    }
    
    public async Task ResetReminderToDefaultAsync(int configId)
    {
        // 1. Preluăm configurația și vehiculul asociat
        var configToReset = await _reminderRepository.GetConfigWithVehicleDetailsAsync(configId);

        if (configToReset == null || configToReset.Vehicle == null)
        {
            throw new KeyNotFoundException($"Reminder configuration with ID {configId} not found.");
        }
        
        // 2. Regenerăm planul de mentenanță default pentru vehiculul respectiv
        var defaultPlan = _calculatorService.GeneratePlanForVehicle(configToReset.Vehicle);
        
        // 3. Căutăm în planul default elementul corespunzător reminderului nostru
        // Ne bazăm pe nume, presupunând că este un identificator unic în contextul unui plan.
        var defaultPlanItem = defaultPlan.FirstOrDefault(p => p.Name == configToReset.Name);

        if (defaultPlanItem == null)
        {
            // Acest lucru nu ar trebui să se întâmple dacă logica e corectă
            throw new InvalidOperationException($"Could not find default values for reminder '{configToReset.Name}'.");
        }
        
        // 4. Actualizăm configurația existentă cu valorile default
        configToReset.MileageIntervalConfig = defaultPlanItem.MileageInterval;
        configToReset.DateIntervalConfig = defaultPlanItem.TimeInterval;

        // 5. Salvăm modificările.
        // Pentru asta, vom avea nevoie de o metodă de update în repository.
        // Să o adăugăm.
        await _reminderRepository.UpdateConfigAsync(configToReset);
    }

    public async Task UpdateReminderActiveAsync(int reminderId)
    {
        await _reminderRepository.UpdateReminderActiveAsync(reminderId);
    }
    
    public async Task<bool> UserOwnsReminderAsync(int userId, int configId)
    {
        return await _reminderRepository.DoesUserOwnReminderAsync(userId, configId);
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
            var config = reminder.VehicleMaintenanceConfig;

            // 1. Actualizăm valorile DueMileage și DueDate doar dacă intervalele sunt active
            if (config.MileageIntervalConfig != -1)
            {
                var mileageToDecrease = config.Vehicle.VehicleInfo.AverageTravelDistance * daysPassed;
                var newDueMileage = reminder.DueMileage - mileageToDecrease;
                // Contorul poate ajunge la 0 sau mai jos, dar nu-l vom afișa ca negativ. Math.Max asigură asta.
                reminder.DueMileage = Math.Max(0, newDueMileage);
                hasChanged = true;
            }

            if (config.DateIntervalConfig != -1)
            {
                var newDueDate = reminder.DueDate - (int)Math.Round(daysPassed);
                reminder.DueDate = Math.Max(0, newDueDate);
                hasChanged = true;
            }

            // 2. Evaluăm noua stare cu o logică mai clară și mai robustă

            // Evaluăm fiecare condiție separat
            bool mileageIsDueSoon = (config.MileageIntervalConfig != -1) && (reminder.DueMileage <= 100);
            bool timeIsDueSoon = (config.DateIntervalConfig != -1) && (reminder.DueDate <= 30);

            bool mileageIsOverdue = (config.MileageIntervalConfig != -1) && (reminder.DueMileage <= 10);
            bool timeIsOverdue = (config.DateIntervalConfig != -1) && (reminder.DueDate <= 1);

            // Combinăm condițiile pentru a determina starea finală
            if (mileageIsOverdue || timeIsOverdue)
            {
                reminder.StatusId = 3; // Overdue
            }
            else if (mileageIsDueSoon || timeIsDueSoon)
            {
                reminder.StatusId = 2; // Due Soon
            }
            else
            {
                reminder.StatusId = 1; // Up to date
            }

            // 3. Creăm notificare dacă starea s-a înrăutățit (la fel ca înainte)
            if (reminder.StatusId > originalStatusId)
            {
                var vehicle = config.Vehicle;
                var producerName = vehicle.VehicleModel.Producer.Name;
                var seriesName = vehicle.VehicleModel.SeriesName;

                string messageType = reminder.StatusId == 2 ? "Upcoming" : "Overdue";
                string message = $"{messageType}: '{config.Name}' for your {producerName} {seriesName} is due.";

                notificationsToAdd.Add(new Notification
                {
                    VehicleId = vehicle.Id,
                    Message = message,
                    Date = DateTime.UtcNow,
                    IsActive = true,
                    IsRead = false,
                    UserId = vehicle.ClientId,
                    RemiderId = config.Id
                });
            }

            // 4. Adăugăm reminder-ul la lista de actualizare dacă s-a schimbat ceva (la fel ca înainte)
            if (hasChanged || reminder.StatusId != originalStatusId)
            {
                remindersToUpdate.Add(reminder);
            }
        }

        // 5. Salvăm toate modificările (la fel ca înainte)
        await _reminderRepository.UpdateRemindersAndAddNotificationsAsync(remindersToUpdate, notificationsToAdd);
    }
}