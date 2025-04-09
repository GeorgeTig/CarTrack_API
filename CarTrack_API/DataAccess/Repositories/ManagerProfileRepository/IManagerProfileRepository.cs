using CarTrack_API.Models;

namespace CarTrack_API.DataAccess.Repositories.ManagerProfileRepository;

public interface IManagerProfileRepository
{
    Task AddManagerProfileAsync(ManagerProfile managerProfile);
}