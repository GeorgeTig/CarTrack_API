using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.VehicleRepository;

public interface IVehicleRepository
{
    Task<List<Vehicle>> GetAllByClientIdAsync(int clientId);
    Task AddVehicleAsync(Vehicle vehicle);
    Task<Vehicle?> GetByIdAsync(int id);
    Task<Vehicle?> GetByVinAsync(string vin);
    Task<VehicleEngine> GetVehicleEngineByVehicleIdAsync(int vehId);
    Task<VehicleInfo> GetVehicleInfoByVehicleIdAsync(int vehId);
    Task<VehicleModel> GetVehicleModelByVehicleIdAsync(int vehId);
    Task<Body> GetVehicleBodyByVehicleIdAsync(int vehId);
    Task AddVehicleMaintenanceAsync(MaintenanceUnverifiedRecord maintenance);
    Task<List<MaintenanceUnverifiedRecord>> GetVehicleMaintenanceByVehicleIdAsync(int vehId);
    Task<List<MileageReading>> GetMileageReadingsForDateRangeAsync(int vehicleId, DateTime startDate);
    Task AddMileageReadingAsync(MileageReading reading);
    Task<MileageReading?> GetLastMileageReadingAsync(int vehicleId);
    Task UpdateVehicleInfoAsync(VehicleInfo vehicleInfo);
}
