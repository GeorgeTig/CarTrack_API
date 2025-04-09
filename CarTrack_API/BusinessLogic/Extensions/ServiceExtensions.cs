using CarTrack_API.BusinessLogic.Services;
using CarTrack_API.BusinessLogic.Services.ClientProfileService;
using CarTrack_API.BusinessLogic.Services.JwtService;
using CarTrack_API.BusinessLogic.Services.ManagerProfileService;
using CarTrack_API.BusinessLogic.Services.UserRoleService;
using CarTrack_API.BusinessLogic.Services.UserService;
using CarTrack_API.BusinessLogic.Services.VehicleService;
using CarTrack_API.DataAccess.Repositories.AppointmentRepository;
using CarTrack_API.DataAccess.Repositories.BodyRepository;
using CarTrack_API.DataAccess.Repositories.ClientProfileRepository;
using CarTrack_API.DataAccess.Repositories.DealRepository;
using CarTrack_API.DataAccess.Repositories.MaintenanceRecordRepository;
using CarTrack_API.DataAccess.Repositories.ManagerProfileRepository;
using CarTrack_API.DataAccess.Repositories.MechanicProfileRepository;
using CarTrack_API.DataAccess.Repositories.NotificationRepository;
using CarTrack_API.DataAccess.Repositories.ProducerRepository;
using CarTrack_API.DataAccess.Repositories.RepairShopRepository;
using CarTrack_API.DataAccess.Repositories.UserRepository;
using CarTrack_API.DataAccess.Repositories.UserRoleRepository;
using CarTrack_API.DataAccess.Repositories.VehicleEngineRepository;
using CarTrack_API.DataAccess.Repositories.VehicleModelRepository;
using CarTrack_API.DataAccess.Repositories.VehiclePaperRepository;
using CarTrack_API.DataAccess.Repositories.VehicleRepository;

namespace CarTrack_API.BusinessLogic.Extensions;

public static class ServiceExtensions
{
    public static void AddBusinessService(this IServiceCollection services)
    {
        // Add services
        services.AddScoped<IClientProfileService, ClientProfileService>();
        services.AddScoped<IManagerProfileService, ManagerProfileService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IVehicleService, VehicleService>();

        // Add repositories
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IBodyRepository, BodyRepository>();
        services.AddScoped<IClientProfileRepository, ClientProfileRepository>();
        services.AddScoped<IDealRepository, DealRepository>();
        services.AddScoped<IMaintenanceRecordRepository, MaintenanceRecordRepository>();
        services.AddScoped<IManagerProfileRepository, ManagerProfileRepository>();
        services.AddScoped<IMechanicProfileRepository, MechanicProfileRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IProducerRepository, ProducerRepository>();
        services.AddScoped<IRepairShopRepository, RepairShopRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IVehicleEngineRepository, VehicleEngineRepository>();
        services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();
        services.AddScoped<IVehiclePaperRepository, VehiclePaperRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();

    }
    
}