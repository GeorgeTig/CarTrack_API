using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace CarTrack_API.DataAccess.Repositories.VehicleMaintenanceConfigRepository;

public class VehicleMaintenanceConfigRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context),IVehicleMaintenanceConfigRepository
{
    public async Task AddAsync(VehicleMaintenanceConfig vehicleMaintenanceConfig)
    {
        
        _context.VehicleMaintenanceConfig.Add(vehicleMaintenanceConfig);
        await _context.SaveChangesAsync();
        
        await _context.Entry(vehicleMaintenanceConfig)
            .Reference(vmc => vmc.Vehicle)
            .LoadAsync();
        
        await _context.Entry(vehicleMaintenanceConfig.Vehicle)
            .Reference(v => v.Client)
            .LoadAsync();
        
        await _context.Entry(vehicleMaintenanceConfig.Vehicle)
            .Reference(v => v.VehicleInfo)
            .LoadAsync();
        
    }
    
    public async Task<bool> DoesConfigNameExistForVehicleAsync(int vehicleId, string name)
    {
        return await _context.VehicleMaintenanceConfig
            .AnyAsync(c => c.VehicleId == vehicleId && c.Name.ToLower() == name.ToLower());
    }
    
    public async Task<VehicleMaintenanceConfig?> GetByIdAsync(int configId)
    {
        return await _context.VehicleMaintenanceConfig.FindAsync(configId);
    }

    public async Task DeleteAsync(VehicleMaintenanceConfig config)
    {
        _context.VehicleMaintenanceConfig.Remove(config);
        await _context.SaveChangesAsync();
    }
    
}