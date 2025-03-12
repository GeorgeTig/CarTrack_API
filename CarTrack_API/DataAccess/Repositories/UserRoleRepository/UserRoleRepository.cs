using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.UserRoleRepository;

public class UserRoleRepository(ApplicationDbContext context ) : BaseRepository.BaseRepository(context), IUserRoleRepository
{
    
}