using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.ManagerProfileRepository;

public class ManagerProfileRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IManagerProfileRepository
{
    
}