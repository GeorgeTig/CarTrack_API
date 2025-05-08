using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.ManagerProfileRepository;

public interface IManagerProfileRepository
{
    Task AddManagerProfileAsync(ManagerProfile managerProfile);
}