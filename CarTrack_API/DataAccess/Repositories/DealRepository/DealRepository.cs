using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.DealRepository;

public class DealRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IDealRepository
{
    
}