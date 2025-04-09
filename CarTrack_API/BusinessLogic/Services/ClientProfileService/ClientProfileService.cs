using CarTrack_API.DataAccess.Repositories.ClientProfileRepository;
using CarTrack_API.Models;

namespace CarTrack_API.BusinessLogic.Services.ClientProfileService;

public class ClientProfileService(IClientProfileRepository clientProfileRepository) : IClientProfileService
{
    private readonly IClientProfileRepository _clientProfileRepository = clientProfileRepository;
    
    public async Task AddClientProfileAsync(ClientProfile clientProfile)
    {
        await _clientProfileRepository.AddClientProfileAsync(clientProfile);
    }
}