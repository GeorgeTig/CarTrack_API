using CarTrack_API.DataAccess.DataContext;

namespace CarTrack_API.DataAccess.Repositories.ClientProfileRepository;

public class ClientProfileRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IClientProfileRepository
{
    
}