using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.ReminderRepository;

public interface IReminderRepository
{
    Task AddAsync(Reminder reminder);
}