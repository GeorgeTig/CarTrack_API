using CarTrack_API.Data;

namespace CarTrack_API.DataAccess.Repositories.MaintenanceRecordRepository;

public class MaintenanceRecordRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IMaintenanceRecordRepository
{
    
}