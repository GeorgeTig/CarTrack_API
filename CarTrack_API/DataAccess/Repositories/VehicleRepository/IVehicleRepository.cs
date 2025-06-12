using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.VehicleRepository;


public interface IVehicleRepository
{
    Task<List<Vehicle>> GetVehiclesForListViewAsync(int clientId);
    Task<Vehicle?> GetVehicleForValidationAsync(string vin);
    Task AddVehicleAsync(Vehicle vehicle);
    Task<Vehicle?> GetVehicleByVinForUserAsync(string vin, int userId); // Schimbat în Vehicle? pentru a permite null
    Task<VehicleEngine?> GetEngineByVehicleIdAsync(int vehicleId);
    Task<VehicleInfo?> GetInfoByVehicleIdAsync(int vehicleId);
    Task<VehicleModel?> GetModelByVehicleIdAsync(int vehicleId);
    Task<Body?> GetBodyByVehicleIdAsync(int vehicleId);
    Task<List<MaintenanceUnverifiedRecord>> GetMaintenanceHistoryByVehicleIdAsync(int vehicleId);
    Task AddVehicleMaintenanceAsync(MaintenanceUnverifiedRecord maintenance);
    Task AddMileageReadingAsync(MileageReading reading);
    Task<MileageReading?> GetLastMileageReadingAsync(int vehicleId);
    Task<List<MileageReading>> GetMileageReadingsForDateRangeAsync(int vehicleId, DateTime? startDateUtc = null); // Păstrăm doar această versiune
    Task UpdateVehicleInfoAsync(VehicleInfo vehicleInfo);
    Task<Vehicle?> GetVehicleWithDetailsByIdAsync(int vehicleId); // Schimbat în Vehicle? pentru a permite null
    Task<MileageReading?> GetLastReadingBeforeDateAsync(int vehicleId, DateTime date);
    Task<MileageReading?> GetFirstReadingAfterDateAsync(int vehicleId, DateTime date);
    Task<Dictionary<int, string>> GetMaintenanceConfigNamesByIds(List<int> ids);
    Task<bool> DoesUserOwnVehicleAsync(int userId, int vehicleId);
    Task<Vehicle?> GetByIdAsync(int vehicleId);
    Task UpdateAsync(Vehicle vehicle);
    Task SaveChangesAsync();
}