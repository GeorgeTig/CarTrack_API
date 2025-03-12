using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.NotificationRepository;

public class NotificationRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), INotificationRepository
{
    
}