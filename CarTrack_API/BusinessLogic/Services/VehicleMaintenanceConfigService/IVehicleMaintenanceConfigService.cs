using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.VehicleMaintenanceConfigService;

public interface IVehicleMaintenanceConfigService
{
    Task DefaultMaintenanceConfigAsync(Vehicle vehicle);
}