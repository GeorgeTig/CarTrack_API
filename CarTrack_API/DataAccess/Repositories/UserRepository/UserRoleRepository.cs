using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.UserRepository;

public class UserRoleRepository(ApplicationDbContext context ) : BaseRepository(context), IUserRoleRepository
{
    
}