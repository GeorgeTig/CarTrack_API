using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.NotificationRepository;

public class NotificationRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), INotificationRepository
{
    public async Task<List<Notification>> GetAllNotificationsAsync(int userId)
    {
        var notifications = await _context.Notification
            .Include(n => n.User)
            .Include(n => n.Vehicle)
            .Include(n => n.Reminder)
            .Where(n => n.User.Id == userId)
            .ToListAsync();

        return notifications;
    }
}