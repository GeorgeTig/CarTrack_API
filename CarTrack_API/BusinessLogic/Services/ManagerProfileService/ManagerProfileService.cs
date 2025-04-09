using CarTrack_API.DataAccess.Repositories.ManagerProfileRepository;
using CarTrack_API.Models;

namespace CarTrack_API.BusinessLogic.Services.ManagerProfileService;

public class ManagerProfileService(IManagerProfileRepository managerProfileRepository) : IManagerProfileService
{
    private readonly IManagerProfileRepository _managerProfileRepository = managerProfileRepository;
    public async Task AddManagerProfileAsync(ManagerProfile managerProfile)
    {
        await _managerProfileRepository.AddManagerProfileAsync(managerProfile);
    }
}