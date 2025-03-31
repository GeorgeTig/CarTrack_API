using CarTrack_API.Models;

namespace CarTrack_API.DataAccess.Repositories.UserRoleRepository;

public interface IUserRoleRepository
{
    Task <UserRole?> GetUserRoleAsync(string roleName);
}