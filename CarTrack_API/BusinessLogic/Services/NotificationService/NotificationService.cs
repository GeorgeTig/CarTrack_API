using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.DataAccess.Repositories.NotificationRepository;
using CarTrack_API.EntityLayer.Dtos.NotificationDto;

namespace CarTrack_API.BusinessLogic.Services.NotificationService;

public class NotificationService(INotificationRepository notificationRepository) : INotificationService
{
    private readonly INotificationRepository _notificationRepository = notificationRepository;
    
    public async Task<List<NotificationResponseDto>> GetAllNotificationsAsync(int userId)
    {
        var notifications= await _notificationRepository.GetAllNotificationsAsync(userId);
        return MappingNotification.ToNotificationResponseDtoList(notifications);
    }
   public async Task MarkNotificationsAsReadAsync(List<int> notificationIds)
    {
        await _notificationRepository.MarkNotificationAsReadAsync(notificationIds);
    }

    public async Task DeactivateNotificationsForVehicleAsync(int vehicleId)
    {
        await _notificationRepository.DeactivateAllNotificationsForVehicleAsync(vehicleId);
    }
   
}