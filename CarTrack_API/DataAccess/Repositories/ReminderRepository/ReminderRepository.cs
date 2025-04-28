using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.ReminderRepository;

public class ReminderRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IReminderRepository 
{
    public async Task AddAsync(Reminder reminder)
    {
        _context.Reminder.Add(reminder);
        await _context.SaveChangesAsync();
    }
}