using CarTrack_API.DataAccess.DataContext;

namespace CarTrack_API.DataAccess.Repositories.BaseRepository;

public class BaseRepository
{
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        
}