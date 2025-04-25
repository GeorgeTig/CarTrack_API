using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.VehicleRepository;

public interface IVehicleRepository
{
    Task<List<Vehicle>> GetAllByClientIdAsync(int clientId);
    Task AddVehicleAsync(Vehicle vehicle);
    Task<Vehicle?> GetByIdAsync(int id);
    Task<Vehicle?> GetByVinAsync(string vin);
}