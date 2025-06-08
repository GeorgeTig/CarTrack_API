using CarTrack_API.BusinessLogic.Services;
using CarTrack_API.BusinessLogic.Services.ClientProfileService;
using CarTrack_API.BusinessLogic.Services.JwtService;
using CarTrack_API.BusinessLogic.Services.MaintenanceCalculatorService;
using CarTrack_API.BusinessLogic.Services.ManagerProfileService;
using CarTrack_API.BusinessLogic.Services.NotificationService;
using CarTrack_API.BusinessLogic.Services.ReminderService;
using CarTrack_API.BusinessLogic.Services.UserRoleService;
using CarTrack_API.BusinessLogic.Services.UserService;
using CarTrack_API.BusinessLogic.Services.VehicleEngineService;
using CarTrack_API.BusinessLogic.Services.VehicleMaintenanceConfigService;
using CarTrack_API.BusinessLogic.Services.VehicleModelService;
using CarTrack_API.BusinessLogic.Services.VehicleService;
using CarTrack_API.BusinessLogic.Services.VinDecoderService;
using CarTrack_API.DataAccess.Repositories.AppointmentRepository;
using CarTrack_API.DataAccess.Repositories.BodyRepository;
using CarTrack_API.DataAccess.Repositories.ClientProfileRepository;
using CarTrack_API.DataAccess.Repositories.DealRepository;
using CarTrack_API.DataAccess.Repositories.MaintenanceRecordRepository;
using CarTrack_API.DataAccess.Repositories.ManagerProfileRepository;
using CarTrack_API.DataAccess.Repositories.MechanicProfileRepository;
using CarTrack_API.DataAccess.Repositories.NotificationRepository;
using CarTrack_API.DataAccess.Repositories.ProducerRepository;
using CarTrack_API.DataAccess.Repositories.RefreshTokenRepository;
using CarTrack_API.DataAccess.Repositories.ReminderRepository;
using CarTrack_API.DataAccess.Repositories.RepairShopRepository;
using CarTrack_API.DataAccess.Repositories.UserRepository;
using CarTrack_API.DataAccess.Repositories.UserRoleRepository;
using CarTrack_API.DataAccess.Repositories.VehicleEngineRepository;
using CarTrack_API.DataAccess.Repositories.VehicleMaintenanceConfigRepository;
using CarTrack_API.DataAccess.Repositories.VehicleModelRepository;
using CarTrack_API.DataAccess.Repositories.VehiclePaperRepository;
using CarTrack_API.DataAccess.Repositories.VehicleRepository;
using CarTrack_API.EntityLayer.Models;

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
        services.AddScoped<IVehicleModelService, VehicleModelService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IVinDecoderService, VinDecoderService>();
        services.AddScoped<IVehicleMaintenanceConfigService, VehicleMaintenanceConfigService>();
        services.AddScoped<IReminderService, ReminderService>();
        services.AddScoped<IVehicleEngineService, VehicleEngineService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddSingleton<IMaintenanceCalculatorService, MaintenanceCalculatorService>();

        // Add repositories
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IVehicleEngineRepository, VehicleEngineRepository>();
        services.AddScoped<IReminderRepository, ReminderRepository>();
        services.AddScoped<IVehicleMaintenanceConfigRepository, VehicleMaintenanceConfigRepository>();
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