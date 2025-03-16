using CarTrack_API.DataAccess.DataContext;

namespace CarTrack_API.DataAccess.Repositories.ManagerProfileRepository;

public class ManagerProfileRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IManagerProfileRepository
{
    
}