using CarTrack_API.DataAccess.DataContext;

namespace CarTrack_API.DataAccess.Repositories.VehicleEngineRepository;

public class VehicleEngineRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IVehicleEngineRepository
{
    
}