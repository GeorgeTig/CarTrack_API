using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.Models;

namespace CarTrack_API.DataAccess.Repositories.ClientProfileRepository;

public class ClientProfileRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IClientProfileRepository
{
    public async Task AddClientProfileAsync(ClientProfile clientProfile)
    {
        _context.ClientProfile.Add(clientProfile);
        await _context.SaveChangesAsync();
    }
}