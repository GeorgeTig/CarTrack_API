using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.BusinessLogic.Services.MaintenanceCalculatorService;
using CarTrack_API.DataAccess.Repositories.ReminderRepository;
using CarTrack_API.DataAccess.Repositories.VehicleMaintenanceConfigRepository;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.ReminderService;

public class ReminderService : IReminderService
{
    private readonly IReminderRepository _reminderRepository;
    private readonly IMaintenanceCalculatorService _calculatorService;

    public ReminderService(
        IReminderRepository reminderRepository, 
        IMaintenanceCalculatorService calculatorService)
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

    public async Task<ReminderResponseDto?> GetReminderByReminderIdAsync(int reminderId)
    {
        var reminder = await _reminderRepository.GetReminderByReminderIdAsync(reminderId);
        return reminder?.ToReminderResponseDto();
    }

    public async Task UpdateReminderAsync(ReminderRequestDto reminderRequest)
    {
        await _reminderRepository.UpdateReminderAsync(reminderRequest);
    }
    
    public async Task UpdateReminderAsync(VehicleMaintenanceRequestDto vehicleMaintenanceRequest)
    {
        var configIdsToReset = vehicleMaintenanceRequest.MaintenanceItems
            .Where(item => item.ConfigId.HasValue)
            .Select(item => item.ConfigId.Value)
            .ToList();
            
        if (!configIdsToReset.Any()) return;

        await _reminderRepository.ResetRemindersAsync(
            vehicleMaintenanceRequest.VehicleId,
            configIdsToReset,
            vehicleMaintenanceRequest.Mileage,
            vehicleMaintenanceRequest.Date
        );
    }
    
    public async Task SoftDeleteRemindersForVehicleAsync(int vehicleId)
    {
        await _reminderRepository.SoftDeleteAllRemindersForVehicleAsync(vehicleId);
    }
    
    public async Task SoftDeleteCustomReminderAsync(int configId)
    {
        var config = await _reminderRepository.GetConfigWithVehicleDetailsAsync(configId);
        if (config == null) return;

        if (!config.IsCustom)
        {
            throw new InvalidOperationException("Default reminders cannot be deleted. They can only be toggled active/inactive.");
        }
        await _reminderRepository.SoftDeleteReminderByConfigIdAsync(configId);
    }
    
    public async Task ResetReminderToDefaultAsync(int configId)
    {
        var configToReset = await _reminderRepository.GetConfigWithVehicleDetailsAsync(configId);
        if (configToReset?.Vehicle == null)
        {
            throw new KeyNotFoundException($"Reminder configuration with ID {configId} not found.");
        }
        
        var defaultPlan = _calculatorService.GeneratePlanForVehicle(configToReset.Vehicle);
        var defaultPlanItem = defaultPlan.FirstOrDefault(p => p.Name == configToReset.Name);
        if (defaultPlanItem == null)
        {
            throw new InvalidOperationException($"Could not find default values for reminder '{configToReset.Name}'.");
        }
        
        configToReset.MileageIntervalConfig = defaultPlanItem.MileageInterval;
        configToReset.DateIntervalConfig = defaultPlanItem.TimeInterval;
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

    public async Task<List<ReminderTypeResponseDto>> GetAllReminderTypesAsync()
    {
        var maintenanceTypes = await _reminderRepository.GetAllTypesAsync();
        return maintenanceTypes.Select(type => new ReminderTypeResponseDto
        {
            Id = type.Id,
            Name = type.Name
        }).ToList();
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

            if (config.MileageIntervalConfig != -1)
            {
                var mileageToDecrease = config.Vehicle.VehicleInfo.AverageTravelDistance * daysPassed;
                reminder.DueMileage = Math.Max(0, reminder.DueMileage - mileageToDecrease);
                hasChanged = true;
            }

            if (config.DateIntervalConfig != -1)
            {
                var newDueDate = reminder.DueDate - (int)Math.Round(daysPassed);
                reminder.DueDate = Math.Max(0, newDueDate);
                hasChanged = true;
            }

            bool mileageIsDueSoon = config.MileageIntervalConfig != -1 && reminder.DueMileage <= 1000;
            bool timeIsDueSoon = config.DateIntervalConfig != -1 && reminder.DueDate <= 30;
            bool mileageIsOverdue = config.MileageIntervalConfig != -1 && reminder.DueMileage <= 50;
            bool timeIsOverdue = config.DateIntervalConfig != -1 && reminder.DueDate <= 1;

            if (mileageIsOverdue || timeIsOverdue) reminder.StatusId = 3;
            else if (mileageIsDueSoon || timeIsDueSoon) reminder.StatusId = 2;
            else reminder.StatusId = 1;

            if (reminder.StatusId > originalStatusId)
            {
                var vehicle = config.Vehicle;
                string messageType = reminder.StatusId == 2 ? "Upcoming" : "Overdue";
                string message = $"{messageType}: '{config.Name}' for your {vehicle.VehicleModel.Producer.Name} {vehicle.VehicleModel.SeriesName} is due.";
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

            if (hasChanged || reminder.StatusId != originalStatusId)
            {
                remindersToUpdate.Add(reminder);
            }
        }
        await _reminderRepository.UpdateRemindersAndAddNotificationsAsync(remindersToUpdate, notificationsToAdd);
    }
}