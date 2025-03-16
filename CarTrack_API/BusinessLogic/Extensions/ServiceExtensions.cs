using CarTrack_API.DataAccess.Repositories.ClientProfileRepository;
using CarTrack_API.DataAccess.Repositories.DealRepository;
using CarTrack_API.DataAccess.Repositories.MaintenanceRecordRepository;
using CarTrack_API.DataAccess.Repositories.RepairShopRepository;

namespace CarTrack_API.BusinessLogic.Extensions;

public static class ServiceExtensions
{
    public static void AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<IClientProfileRepository, ClientProfileRepository>();
        services.AddScoped<IDealRepository, DealRepository>();
        services.AddScoped<IMaintenanceRecordRepository, MaintenanceRecordRepository>();
        services.AddScoped<IRepairShopRepository, RepairShopRepository>();
        
        
    }
    
}