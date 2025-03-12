using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.VehicleEngineRepository;

public class VehicleEngineRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IVehicleEngineRepository
{
    
}