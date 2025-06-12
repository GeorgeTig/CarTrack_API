using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Exceptions.VehicleException;
using CarTrack_API.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.VehicleRepository;

public class VehicleRepository(ApplicationDbContext context) : IVehicleRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<MileageReading?> GetLastReadingBeforeDateAsync(int vehicleId, DateTime date)
    {
        return await _context.MileageReading
            .Where(r => r.VehicleId == vehicleId && r.ReadingDate < date)
            .OrderByDescending(r => r.ReadingDate)
            .FirstOrDefaultAsync();
    }

    public async Task<Vehicle?> GetByIdAsync(int vehicleId)
    {
        return await _context.Vehicle.FindAsync(vehicleId);
    }

    public async Task UpdateAsync(Vehicle vehicle)
    {
        _context.Vehicle.Update(vehicle);
        await _context.SaveChangesAsync();
    }

    // --- METODA ACTUALIZATĂ CU FILTRUL IsActive ---
    public async Task<bool> DoesUserOwnVehicleAsync(int userId, int vehicleId)
    {
        return await _context.Vehicle
            .AnyAsync(v => v.Id == vehicleId && v.ClientId == userId && v.IsActive);
    }

    // --- PĂSTRĂM DOAR ACEASTĂ VERSIUNE A METODEI ---
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

    public async Task<List<Vehicle>> GetVehiclesForListViewAsync(int clientId)
    {
        return await _context.Vehicle
            .Where(v => v.ClientId == clientId && v.IsActive)
            .Include(v => v.VehicleModel)
            .Include(v => v.VehicleInfo)
            .Include(v => v.VehicleModel.Producer)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Vehicle?> GetVehicleByVinForUserAsync(string vin, int userId)
    {
        return await _context.Vehicle
            .Include(v => v.VehicleInfo)
            .AsNoTracking()
            .FirstOrDefaultAsync(v => 
                v.ClientId == userId && 
                v.VehicleInfo.Vin == vin && 
                v.IsActive);
        
    }

    public async Task<Vehicle?> GetVehicleForValidationAsync(string vin)
    {
        return await _context.Vehicle
            .Include(v => v.VehicleInfo)
            .AsNoTracking()
            .FirstOrDefaultAsync(v => 
                v.VehicleInfo.Vin == vin && 
                v.IsActive);
        
    }

    public async Task AddVehicleAsync(Vehicle vehicle)
    {
        var existingActiveVehicle = await GetVehicleForValidationAsync(vehicle.VehicleInfo.Vin);
        if (existingActiveVehicle != null)
        {
            throw new VehicleAlreadyExistException($"An active vehicle with VIN {vehicle.VehicleInfo.Vin} already exists!");
        }
        await _context.Vehicle.AddAsync(vehicle);
        await _context.SaveChangesAsync();
    }

    public async Task<VehicleEngine?> GetEngineByVehicleIdAsync(int vehicleId)
    {
        var vehicle = await _context.Vehicle
            .Where(v => v.Id == vehicleId)
            .Include(v => v.VehicleModel.VehicleEngine)
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
            .Include(v => v.VehicleModel.Body)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        return vehicle?.VehicleModel?.Body;
    }

    public async Task AddVehicleMaintenanceAsync(MaintenanceUnverifiedRecord maintenance)
    {
        await _context.MaintenanceUnverifiedRecord.AddAsync(maintenance);
    }

    public async Task<List<MaintenanceUnverifiedRecord>> GetMaintenanceHistoryByVehicleIdAsync(int vehId)
    {
        return await _context.MaintenanceUnverifiedRecord
            .Where(m => m.VehicleId == vehId)
            .AsNoTracking()
            .OrderByDescending(m => m.DoneDate)
            .ToListAsync();
    }

    public async Task AddMileageReadingAsync(MileageReading reading)
    {
        await _context.MileageReading.AddAsync(reading);
    }

    public async Task<MileageReading?> GetLastMileageReadingAsync(int vehicleId)
    {
        return await _context.MileageReading
            .Where(r => r.VehicleId == vehicleId)
            .OrderByDescending(r => r.ReadingDate)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateVehicleInfoAsync(VehicleInfo vehicleInfo)
    {
        _context.VehicleInfo.Update(vehicleInfo);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Vehicle?> GetVehicleWithDetailsByIdAsync(int vehicleId)
    {
        return await _context.Vehicle
            .Include(v => v.VehicleInfo)
            .Include(v => v.VehicleModel.VehicleEngine)
            .Include(v => v.VehicleModel.Body)
            .FirstOrDefaultAsync(v => v.Id == vehicleId);
    }
}