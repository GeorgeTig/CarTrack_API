using CarTrack_API.DataAccess.DataContext;

namespace CarTrack_API.DataAccess.Repositories.MechanicProfileRepository;

public class MechanicProfileRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IMechanicProfileRepository
{
    
}