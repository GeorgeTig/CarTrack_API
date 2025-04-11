using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.VehicleRepository;

public class VehicleRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IVehicleRepository
{
    public async Task<List<Vehicle>> GetAllByClientIdAsync( int clientId)
    {
        return await Task.FromResult(_context.Vehicle
            .Include(v=> v.VehicleModel)
            .Include(v => v.Client)
            .Include(v => v.Appointments)
            .Include(v => v.MaintenanceRecord)
            .Include(v => v.VehiclePapers)
            .Where(v => v.ClientId == clientId).ToList());
    }
}