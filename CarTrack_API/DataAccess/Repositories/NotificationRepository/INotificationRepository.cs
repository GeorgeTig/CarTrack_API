using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.NotificationRepository;

public interface INotificationRepository
{
    Task<List<Notification>> GetAllNotificationsAsync(int userId);
    Task MarkNotificationAsReadAsync(List<int> notificationIds);
}