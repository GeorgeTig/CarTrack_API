using CarTrack_API.BusinessLogic.Services;
using CarTrack_API.BusinessLogic.Services.UserService;
using CarTrack_API.DataAccess.Repositories.ClientProfileRepository;
using CarTrack_API.DataAccess.Repositories.DealRepository;
using CarTrack_API.DataAccess.Repositories.MaintenanceRecordRepository;
using CarTrack_API.DataAccess.Repositories.RepairShopRepository;
using CarTrack_API.DataAccess.Repositories.UserRepository;

namespace CarTrack_API.BusinessLogic.Extensions;

public static class ServiceExtensions
{
    public static void AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<JwtService>();
        services.AddScoped<IClientProfileRepository, ClientProfileRepository>();
        services.AddScoped<IDealRepository, DealRepository>();
        services.AddScoped<IMaintenanceRecordRepository, MaintenanceRecordRepository>();
        services.AddScoped<IRepairShopRepository, RepairShopRepository>();
        
        
    }
    
}