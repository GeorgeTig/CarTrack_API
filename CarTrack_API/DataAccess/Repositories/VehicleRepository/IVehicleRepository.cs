using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.VehicleRepository;

public interface IVehicleRepository
{
    Task<List<Vehicle>> GetAllByClientIdAsync(int clientId);
}