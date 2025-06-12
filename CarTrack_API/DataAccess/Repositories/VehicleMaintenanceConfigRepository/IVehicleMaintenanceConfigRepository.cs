using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.VehicleMaintenanceConfigRepository;

public interface IVehicleMaintenanceConfigRepository
{
    Task AddAsync(VehicleMaintenanceConfig vehicleMaintenanceConfig);
    Task<bool> DoesConfigNameExistForVehicleAsync(int vehicleId, string name);
    Task<VehicleMaintenanceConfig?> GetByIdAsync(int configId);
    Task DeleteAsync(VehicleMaintenanceConfig config);

}