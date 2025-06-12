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
            .Include(n=> n.Vehicle.VehicleModel)
            .Include(n => n.Vehicle.VehicleModel.Producer)
            .Include(n => n.Vehicle.VehicleInfo)
            .Include(n => n.Reminder)
            .Where(n => n.User.Id == userId && n.IsActive)
            .ToListAsync();

        return notifications;
    }
    
    public async Task DeactivateAllNotificationsForVehicleAsync(int vehicleId)
    {
        // Găsim toate notificările active pentru vehiculul specificat
        var notificationsToDeactivate = await _context.Notification
            .Where(n => n.VehicleId == vehicleId && n.IsActive)
            .ToListAsync();

        if (notificationsToDeactivate.Any())
        {
            foreach (var notification in notificationsToDeactivate)
            {
                notification.IsActive = false;
            }
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task MarkNotificationAsReadAsync(List<int> notificationIds)
    {
        foreach (var id in notificationIds)
        {
            var notification = await _context.Notification
                .FirstOrDefaultAsync(n => n.Id == id);
            
            if (notification != null)
            {
                notification.IsRead = true;
                _context.Notification.Update(notification);
            }
           
        }
        await _context.SaveChangesAsync();
    }
}