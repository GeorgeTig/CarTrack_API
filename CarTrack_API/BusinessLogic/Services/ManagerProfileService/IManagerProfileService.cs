using CarTrack_API.Models;

namespace CarTrack_API.BusinessLogic.Services.ManagerProfileService;

public interface IManagerProfileService
{
    Task AddManagerProfileAsync(ManagerProfile managerProfile);
}