using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.VehicleRepository;


public interface IVehicleRepository
{
    // --- Metode de Citire ---
    Task<List<Vehicle>> GetVehiclesForListViewAsync(int clientId);
    Task<Vehicle?> GetVehicleForValidationAsync(string vin);
    Task<Vehicle?> GetVehicleByVinForUserAsync(string vin, int userId);
    Task<VehicleEngine?> GetEngineByVehicleIdAsync(int vehicleId);
    Task<VehicleInfo?> GetInfoByVehicleIdAsync(int vehicleId);
    Task<VehicleModel?> GetModelByVehicleIdAsync(int vehicleId);
    Task<Body?> GetBodyByVehicleIdAsync(int vehicleId);
    Task<List<MaintenanceUnverifiedRecord>> GetMaintenanceHistoryByVehicleIdAsync(int vehicleId);
    Task<MileageReading?> GetLastMileageReadingAsync(int vehicleId);
    Task<List<MileageReading>> GetMileageReadingsForDateRangeAsync(int vehicleId, DateTime? startDateUtc = null);
    Task<Vehicle?> GetVehicleWithDetailsByIdAsync(int vehicleId);
    Task<MileageReading?> GetLastReadingBeforeDateAsync(int vehicleId, DateTime date);
    Task<MileageReading?> GetFirstReadingAfterDateAsync(int vehicleId, DateTime date);
    Task<Dictionary<int, string>> GetMaintenanceConfigNamesByIds(List<int> ids);
    Task<bool> DoesUserOwnVehicleAsync(int userId, int vehicleId);
    Task<Vehicle?> GetByIdAsync(int vehicleId);
    Task<VehicleInfo?> GetInfoByVehicleIdForUpdateAsync(int vehicleId);


    // --- Metode de Scriere/Modificare ---
    void AddVehicle(Vehicle vehicle);
    void AddVehicleMaintenance(MaintenanceUnverifiedRecord maintenance);
    void AddMileageReading(MileageReading reading);
    void UpdateVehicleInfo(VehicleInfo vehicleInfo);
    void Update(Vehicle vehicle);

    // --- Metodă pentru Salvare Explicită ---
    Task<int> SaveChangesAsync();
}