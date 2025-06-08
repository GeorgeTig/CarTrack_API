using CarTrack_API.EntityLayer.Dtos.NotificationDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingNotification
{
    public static NotificationResponseDto ToNotificationResponseDto(this Notification notification)
    {
        return new NotificationResponseDto
        {
            Id = notification.Id,
            Message = notification.Message,
            Date = notification.Date,
            IsRead = notification.IsRead,
            UserId = notification.UserId,
            VehicleId = notification.VehicleId,
            ReminderId = notification.RemiderId,
            
            VehicleName =notification.Vehicle.VehicleModel.Producer.Name + " " + notification.Vehicle.VehicleModel.SeriesName,
            VehicleYear = notification.Vehicle.VehicleModel.Year
        };
    }
    
    public static List<NotificationResponseDto> ToNotificationResponseDtoList(this List<Notification> notifications)
    {
        var notificationResponseDtos = new List<NotificationResponseDto>();
        foreach (var notif in notifications)
        {
            var notificationResponseDto = notif.ToNotificationResponseDto();
            notificationResponseDtos.Add(notificationResponseDto);
        }
        return notificationResponseDtos;
    }
}