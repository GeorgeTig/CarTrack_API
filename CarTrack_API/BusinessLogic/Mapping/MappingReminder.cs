using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingReminder
{
    public static Reminder ToReminder(this VehicleMaintenanceConfig config, double vehicleMileage)
    {
        var reminder = new Reminder
        {
            VehicleMaintenanceConfigId = config.Id,
            LastMileageCkeck = vehicleMileage % config.MileageIntervalConfig,
            LastDateCheck = DateTime.UtcNow,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            StatusId = 1, // 1 is for UP_TO_DATE
        };

        return reminder;
    }
}