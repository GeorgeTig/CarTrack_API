using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.BodyRepository;

public class BodyRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IBodyRepository
{
    
}