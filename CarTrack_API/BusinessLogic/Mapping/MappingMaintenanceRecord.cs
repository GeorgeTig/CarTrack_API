using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingMaintenanceRecord
{
    public static MaintenanceUnverifiedRecord ToMaintenanceUnverifiedRecord(this VehicleMaintenanceRequestDto vehMaintenance)
    {
        
        DateTime dateAsUtc = new DateTime(vehMaintenance.Date.Year, 
            vehMaintenance.Date.Month, 
            vehMaintenance.Date.Day, 
            0, 0, 0, DateTimeKind.Utc); 
        
        var maintenance = new MaintenanceUnverifiedRecord
        {
            VehicleId = vehMaintenance.VehicleId,
            DoneDate = dateAsUtc,
            Cost = vehMaintenance.Cost,
            Description = vehMaintenance.Notes,
            DoneMileage = vehMaintenance.Mileage,
            MaintenanceNames = vehMaintenance.MaintenanceItems.Select(item => item.Name).ToList(),
            ServiceProvider = vehMaintenance.ServiceProvider,
            
        };
        
        return maintenance;
    }

    public static List<MaintenanceLogDto> ToMaintenanceLogDtoList(
        this List<MaintenanceUnverifiedRecord> maintenanceRecords)
    {
        var maintenanceLogDtos = new List<MaintenanceLogDto>();
        foreach (var record in maintenanceRecords)
        {
            var maintenanceLogDto = new MaintenanceLogDto
            {
                Id = record.Id,
                Date = record.DoneDate,
                Cost = (double)record.Cost,
                Mileage = record.DoneMileage,
                PerformedTasks = record.MaintenanceNames,
                ServiceProvider = record.ServiceProvider,
                Notes = record.Description
            };
            maintenanceLogDtos.Add(maintenanceLogDto);
        }

        return maintenanceLogDtos;
    }
}