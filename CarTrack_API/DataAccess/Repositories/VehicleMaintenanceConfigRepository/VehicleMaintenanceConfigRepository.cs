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
    
}