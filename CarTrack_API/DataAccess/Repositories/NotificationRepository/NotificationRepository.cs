using CarTrack_API.DataAccess.DataContext;

namespace CarTrack_API.DataAccess.Repositories.NotificationRepository;

public class NotificationRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), INotificationRepository
{
    
}