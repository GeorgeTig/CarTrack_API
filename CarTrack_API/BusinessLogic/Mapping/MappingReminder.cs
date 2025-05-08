using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingReminder
{
    public static Reminder ToReminder(this VehicleMaintenanceConfig config, double vehicleMileage)
    {
        var reminder = new Reminder
        {
            VehicleMaintenanceConfigId = config.Id,
            LastMileageCkeck = (vehicleMileage % config.MileageIntervalConfig) == 0 ? vehicleMileage : 0,
            LastDateCheck = DateTime.UtcNow,
            IsActive = true,
            StatusId = 1, // 1 is for UP_TO_DATE
        };

        return reminder;
    }
    
    public static List<ReminderResponseDto> ToListReminderResponseDto(this List<Reminder> reminders)
    {
        var reminderResponseDtos = new List<ReminderResponseDto>();
        foreach (var reminder in reminders)
        {
            var reminderResponseDto = new ReminderResponseDto
            {
                ConfigId = reminder.VehicleMaintenanceConfigId,
                StatusId = reminder.StatusId,
                TypeId = reminder.VehicleMaintenanceConfig.MaintenanceTypeId,
                Name = reminder.VehicleMaintenanceConfig.Name,
                TypeName = reminder.VehicleMaintenanceConfig.MaintenanceType.Name,
                MileageInterval = reminder.VehicleMaintenanceConfig.MileageIntervalConfig,
                TimeInterval = reminder.VehicleMaintenanceConfig.DateIntervalConfig,
                LastMileageCheck = reminder.LastMileageCkeck,
                LastDateCheck = reminder.LastDateCheck,
                IsEditable = reminder.VehicleMaintenanceConfig.IsEditable,
                DueMileage = reminder.LastMileageCkeck + reminder.VehicleMaintenanceConfig.MileageIntervalConfig,
                DueDate = reminder.LastDateCheck.AddDays(reminder.VehicleMaintenanceConfig.DateIntervalConfig),
            };
            reminderResponseDtos.Add(reminderResponseDto);
        }
        return reminderResponseDtos;
    }
}