using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Exceptions.VehicleException;
using CarTrack_API.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.VehicleRepository;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _context;

    public VehicleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // --- Metode de Scriere/Modificare (Fără SaveChanges) ---
    public void AddVehicle(Vehicle vehicle)
    {
        _context.Vehicle.Add(vehicle);
    }
    
    public void AddVehicleMaintenance(MaintenanceUnverifiedRecord maintenance)
    {
        _context.MaintenanceUnverifiedRecord.Add(maintenance);
    }

    public void AddMileageReading(MileageReading reading)
    {
        _context.MileageReading.Add(reading);
    }
    
    public void UpdateVehicleInfo(VehicleInfo vehicleInfo)
    {
        _context.VehicleInfo.Update(vehicleInfo);
    }
    
    public void Update(Vehicle vehicle)
    {
        _context.Vehicle.Update(vehicle);
    }

    // --- Metoda de Salvare ---
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    // --- Metode de Citire ---
    public async Task<Vehicle?> GetByIdAsync(int vehicleId)
    {
        return await _context.Vehicle.FindAsync(vehicleId);
    }

    public async Task<bool> DoesUserOwnVehicleAsync(int userId, int vehicleId)
    {
        return await _context.Vehicle
            .AnyAsync(v => v.Id == vehicleId && v.ClientId == userId && v.IsActive);
    }

    public async Task<List<Vehicle>> GetVehiclesForListViewAsync(int clientId)
    {
        return await _context.Vehicle
            .Where(v => v.ClientId == clientId && v.IsActive)
            .Include(v => v.VehicleModel)
                .ThenInclude(vm => vm.Producer)
            .Include(v => v.VehicleInfo)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Vehicle?> GetVehicleByVinForUserAsync(string vin, int userId)
    {
        return await _context.Vehicle
            .Include(v => v.VehicleInfo)
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.ClientId == userId && v.VehicleInfo.Vin == vin && v.IsActive);
    }

    public async Task<Vehicle?> GetVehicleForValidationAsync(string vin)
    {
        return await _context.Vehicle
            .Include(v => v.VehicleInfo)
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.VehicleInfo.Vin == vin && v.IsActive);
    }

    public async Task<VehicleEngine?> GetEngineByVehicleIdAsync(int vehicleId)
    {
        var vehicle = await _context.Vehicle
            .Where(v => v.Id == vehicleId)
            .Include(v => v.VehicleModel)
                .ThenInclude(vm => vm.VehicleEngine)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        return vehicle?.VehicleModel?.VehicleEngine;
    }
    
    public async Task<VehicleInfo?> GetInfoByVehicleIdAsync(int vehicleId)
    {
        return await _context.VehicleInfo
            .AsNoTracking()
            .FirstOrDefaultAsync(vi => vi.VehicleId == vehicleId);
    }

    public async Task<VehicleModel?> GetModelByVehicleIdAsync(int vehicleId)
    {
        var vehicle = await _context.Vehicle
            .Where(v => v.Id == vehicleId)
            .Include(v => v.VehicleModel)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        return vehicle?.VehicleModel;
    }
    
    public async Task<Body?> GetBodyByVehicleIdAsync(int vehicleId)
    {
        var vehicle = await _context.Vehicle
            .Where(v => v.Id == vehicleId)
            .Include(v => v.VehicleModel)
                .ThenInclude(vm => vm.Body)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        return vehicle?.VehicleModel?.Body;
    }

    public async Task<List<MaintenanceUnverifiedRecord>> GetMaintenanceHistoryByVehicleIdAsync(int vehId)
    {
        return await _context.MaintenanceUnverifiedRecord
            .Where(m => m.VehicleId == vehId)
            .AsNoTracking()
            .OrderByDescending(m => m.DoneDate)
            .ToListAsync();
    }
    
    public async Task<MileageReading?> GetLastMileageReadingAsync(int vehicleId)
    {
        return await _context.MileageReading
            .Where(r => r.VehicleId == vehicleId)
            .OrderByDescending(r => r.ReadingDate)
            .FirstOrDefaultAsync();
    }

    public async Task<List<MileageReading>> GetMileageReadingsForDateRangeAsync(int vehicleId, DateTime? startDateUtc = null)
    {
        var query = _context.MileageReading
            .Where(r => r.VehicleId == vehicleId);

        if (startDateUtc.HasValue)
        {
            query = query.Where(r => r.ReadingDate >= startDateUtc.Value);
        }

        return await query
            .OrderBy(r => r.ReadingDate)
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<Vehicle?> GetVehicleWithDetailsByIdAsync(int vehicleId)
    {
        return await _context.Vehicle
            .Include(v => v.VehicleInfo)
            .Include(v => v.VehicleModel)
                .ThenInclude(vm => vm.VehicleEngine)
            .Include(v => v.VehicleModel)
                .ThenInclude(vm => vm.Body)
            .FirstOrDefaultAsync(v => v.Id == vehicleId);
    }
    
    public async Task<MileageReading?> GetLastReadingBeforeDateAsync(int vehicleId, DateTime date)
    {
        return await _context.MileageReading
            .Where(r => r.VehicleId == vehicleId && r.ReadingDate < date)
            .OrderByDescending(r => r.ReadingDate)
            .FirstOrDefaultAsync();
    }

    public async Task<MileageReading?> GetFirstReadingAfterDateAsync(int vehicleId, DateTime date)
    {
        return await _context.MileageReading
            .Where(r => r.VehicleId == vehicleId && r.ReadingDate > date)
            .OrderBy(r => r.ReadingDate)
            .FirstOrDefaultAsync();
    }

    public async Task<Dictionary<int, string>> GetMaintenanceConfigNamesByIds(List<int> ids)
    {
        return await _context.VehicleMaintenanceConfig
            .Where(c => ids.Contains(c.Id))
            .AsNoTracking()
            .ToDictionaryAsync(c => c.Id, c => c.Name);
    }
}