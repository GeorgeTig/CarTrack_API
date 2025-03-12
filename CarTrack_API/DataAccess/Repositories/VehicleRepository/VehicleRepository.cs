using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.VehicleRepository;

public class VehicleRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IVehicleRepository
{
    
}