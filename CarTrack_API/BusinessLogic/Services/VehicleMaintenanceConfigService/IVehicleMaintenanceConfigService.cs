namespace CarTrack_API.BusinessLogic.Services.VehicleMaintenanceConfigService;

public interface IVehicleMaintenanceConfigService
{
    Task DefaultMaintenanceConfigAsync(int vehicleId);
}