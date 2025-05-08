using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.ManagerProfileService;

public interface IManagerProfileService
{
    Task AddManagerProfileAsync(ManagerProfile managerProfile);
}