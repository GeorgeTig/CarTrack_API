using CarTrack_API.EntityLayer.Dtos.NotificationDto;

namespace CarTrack_API.BusinessLogic.Services.NotificationService;

public interface INotificationService
{
    Task<List<NotificationResponseDto>> GetAllNotificationsAsync(int userId);
    Task MarkNotificationsAsReadAsync(List<int> notificationIds);
    Task DeactivateNotificationsForVehicleAsync(int vehicleId);

}