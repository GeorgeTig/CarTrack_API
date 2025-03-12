using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.UserRepository;

public class UserRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IUserRepository
{
    public void Register()
    {
        throw new NotImplementedException();
    }
}