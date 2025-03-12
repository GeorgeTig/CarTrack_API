using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.RepairShopRepository;

public class RepairShopRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IRepairShopRepository
{
    
}