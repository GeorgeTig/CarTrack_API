using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.Models;

namespace CarTrack_API.DataAccess.Repositories.ManagerProfileRepository;

public class ManagerProfileRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IManagerProfileRepository
{
    public async Task AddManagerProfileAsync(ManagerProfile managerProfile)
    {
        _context.ManagerProfile.Add(managerProfile);
        await _context.SaveChangesAsync();
    }
}