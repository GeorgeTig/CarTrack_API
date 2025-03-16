using CarTrack_API.DataAccess.DataContext;

namespace CarTrack_API.DataAccess.Repositories.VehicleModelRepository;

public class VehicleModelRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IVehicleModelRepository
{
    
}