using CarTrack_API.DataAccess.DataContext;

namespace CarTrack_API.DataAccess.Repositories.BodyRepository;

public class BodyRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IBodyRepository
{
    
}