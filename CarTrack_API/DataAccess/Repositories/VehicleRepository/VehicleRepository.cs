using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.VehicleRepository;

public class VehicleRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IVehicleRepository
{
    public async Task<List<Vehicle>> GetAllByClientIdAsync( int clientId)
    {
        return await Task.FromResult(_context.Vehicle.Where(v => v.ClientId == clientId).ToList());
    }
}