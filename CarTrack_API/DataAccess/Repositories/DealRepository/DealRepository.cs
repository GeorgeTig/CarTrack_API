using CarTrack_API.DataAccess.DataContext;

namespace CarTrack_API.DataAccess.Repositories.DealRepository;

public class DealRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IDealRepository
{
    
}