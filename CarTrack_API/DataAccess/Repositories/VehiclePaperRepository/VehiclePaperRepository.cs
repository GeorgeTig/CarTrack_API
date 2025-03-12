using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.VehiclePaperRepository;

public class VehiclePaperRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IVehiclePaperRepository
{
    
}