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
            MaintenanceNames = vehMaintenance.MaintenanceItems.Select(item => item.Name).ToList()
            
        };
        
        return maintenance;
    }
}