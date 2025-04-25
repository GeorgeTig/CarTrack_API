using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.VehicleMaintenanceConfigRepository;

public interface IVehicleMaintenanceConfigRepository
{
    Task AddAsync(VehicleMaintenanceConfig vehicleMaintenanceConfig);
}