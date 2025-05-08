using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Exceptions.VehicleException;
using CarTrack_API.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.VehicleRepository;

public class VehicleRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IVehicleRepository
{
    public async Task<List<Vehicle>> GetAllByClientIdAsync( int clientId)
    {
        return await Task.FromResult(_context.Vehicle
            .Include(v=> v.VehicleModel)
            .Include(v => v.Client)
            .Include(v => v.Appointments)
            .Include(v => v.MaintenanceVerifiedRecord)
            .Include(v => v.VehiclePapers)
            .Include(v => v.VehicleInfo)
            .Where(v => v.ClientId == clientId).ToList());
    }
    
    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        var vehicle = await _context.Vehicle
            .Include(v => v.VehicleModel)
            .Include(v => v.Client)
            .Include(v => v.Appointments)
            .Include(v => v.MaintenanceVerifiedRecord)
            .Include(v => v.VehiclePapers)
            .Include(v => v.VehicleInfo)
            .FirstOrDefaultAsync(v => v.Id == id);
        
        if (vehicle == null)
        {
            throw new VehicleNotFoundException($"Vehicle with id {id} not found");
        }
        
        return vehicle;
    }
    
    public async Task<Vehicle?> GetByVinAsync(string vin)
    {
        var vehicle = await _context.Vehicle
            .Include(v => v.VehicleModel)
            .Include(v => v.Client)
            .Include(v => v.Appointments)
            .Include(v => v.MaintenanceVerifiedRecord)
            .Include(v => v.VehiclePapers)
            .Include(v => v.VehicleInfo)
            .FirstOrDefaultAsync(v => v.VehicleInfo.Vin == vin);
        
        return vehicle;
    }
    
    public async Task AddVehicleAsync(Vehicle vehicle)
    {
        var veh = await GetByVinAsync(vehicle.VehicleInfo.Vin);
        
        if ( veh != null)
        {
            throw new VehicleAlreadyExistException($"Vehicle with vin {veh.VehicleInfo.Vin} already exits!");
        }
        
        _context.Vehicle.Add(vehicle);
        await _context.SaveChangesAsync();
    }
    
    public async Task<VehicleEngine> GetVehicleEngineByVehicleIdAsync(int vehId)
    {
        var vehicle = await _context.Vehicle
            .Include(v => v.VehicleModel.VehicleEngine)
            .FirstOrDefaultAsync(v => v.Id == vehId);
        
        if (vehicle == null)
        {
            throw new VehicleNotFoundException($"Vehicle with id {vehId} not found");
        }
        
        return vehicle.VehicleModel.VehicleEngine;
    }
    
    public async Task<VehicleInfo> GetVehicleInfoByVehicleIdAsync(int vehId)
    {
        var vehicle = await _context.Vehicle
            .Include(v => v.VehicleInfo)
            .FirstOrDefaultAsync(v => v.Id == vehId);
        
        if (vehicle == null)
        {
            throw new VehicleNotFoundException($"Vehicle with id {vehId} not found");
        }
        
        return vehicle.VehicleInfo;
    }
    
    public async Task<VehicleModel> GetVehicleModelByVehicleIdAsync(int vehId)
    {
        var vehicle = await _context.Vehicle
            .Include(v => v.VehicleModel)
            .FirstOrDefaultAsync(v => v.Id == vehId);
        
        if (vehicle == null)
        {
            throw new VehicleNotFoundException($"Vehicle with id {vehId} not found");
        }
        
        return vehicle.VehicleModel;
    }
    
    public async Task<List<VehicleUsageStats>> GetVehicleUsageStatsByVehicleIdAsync(int vehId)
    {
        var vehicle = await _context.Vehicle
            .Include(v => v.VehicleUsageStats)
            .FirstOrDefaultAsync(v => v.Id == vehId);
        
        if (vehicle == null)
        {
            throw new VehicleNotFoundException($"Vehicle with id {vehId} not found");
        }
        
        return vehicle.VehicleUsageStats;
    }
    
    public async Task<Body> GetVehicleBodyByVehicleIdAsync(int vehId)
    {
        var vehicle = await _context.Vehicle
            .Include(v => v.VehicleModel.Body)
            .FirstOrDefaultAsync(v => v.Id == vehId);
        
        if (vehicle == null)
        {
            throw new VehicleNotFoundException($"Vehicle with id {vehId} not found");
        }
        
        return vehicle.VehicleModel.Body;
    }
    
}