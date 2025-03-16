using CarTrack_API.DataAccess.DataContext;

namespace CarTrack_API.DataAccess.Repositories.AppointmentRepository;

public class AppointmentRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IAppointmentRepository
{
    
}