using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.VehicleRepository;


public interface IVehicleRepository
{
    // --- Metode pentru entitatea Vehicle ---
    Task<List<Vehicle>> GetVehiclesForListViewAsync(int clientId);
    Task<Vehicle?> GetVehicleForValidationAsync(string vin);
    Task AddVehicleAsync(Vehicle vehicle);
    Task<Vehicle> GetVehicleByVinForUserAsync(string vin, int userId);


    // --- Metode pentru obținerea de sub-entități specifice ---
    Task<VehicleEngine?> GetEngineByVehicleIdAsync(int vehicleId);
    Task<VehicleInfo?> GetInfoByVehicleIdAsync(int vehicleId);
    Task<VehicleModel?> GetModelByVehicleIdAsync(int vehicleId);
    Task<Body?> GetBodyByVehicleIdAsync(int vehicleId);
    Task<List<MaintenanceUnverifiedRecord>> GetMaintenanceHistoryByVehicleIdAsync(int vehicleId);

    // --- Metode pentru scriere/actualizare ---
    Task AddVehicleMaintenanceAsync(MaintenanceUnverifiedRecord maintenance);
    Task AddMileageReadingAsync(MileageReading reading);
    Task<MileageReading?> GetLastMileageReadingAsync(int vehicleId);
    Task<List<MileageReading>> GetMileageReadingsForDateRangeAsync(int vehicleId, DateTime startDateUtc);
    Task UpdateVehicleInfoAsync(VehicleInfo vehicleInfo);
    Task<Vehicle> GetVehicleWithDetailsByIdAsync(int vehicleId);

    
    // O metodă generică de salvare, utilă pentru BaseRepository
    Task SaveChangesAsync();
}