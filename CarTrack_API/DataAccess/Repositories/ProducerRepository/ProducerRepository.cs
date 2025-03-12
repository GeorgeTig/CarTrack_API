using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.ProducerRepository;

public class ProducerRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IProducerRepository
{
    
}