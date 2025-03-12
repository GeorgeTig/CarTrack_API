using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.ClientProfileRepository;

public class ClientProfileRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IClientProfileRepository
{
    
}