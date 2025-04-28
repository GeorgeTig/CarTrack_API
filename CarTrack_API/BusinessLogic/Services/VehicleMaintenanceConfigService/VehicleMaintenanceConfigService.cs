using CarTrack_API.BusinessLogic.Services.ReminderService;
using CarTrack_API.DataAccess.Repositories.VehicleMaintenanceConfigRepository;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.VehicleMaintenanceConfigService;

public class VehicleMaintenanceConfigService(IConfiguration config, IVehicleMaintenanceConfigRepository repository, IReminderService reminderService)
    : IVehicleMaintenanceConfigService
{
    private readonly IConfiguration _config = config;
    private readonly IVehicleMaintenanceConfigRepository _repository = repository;
    private readonly IReminderService _reminderService = reminderService;


    public async Task DefaultMaintenanceConfigAsync(int vehicleId)
    {
        var maintenanceSettings = _config.GetSection("DefaultMaintenanceSettings");

        // Oil Change + Oil Filter Change
        var oilConfig = new VehicleMaintenanceConfig
        {
            Name = "Oil Change + Oil Filter Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Oil:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Oil:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Oil:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Oil:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(oilConfig);
        await _reminderService.AddReminderAsync(oilConfig, oilConfig.Vehicle.VehicleInfo.Mileage);

        // Oil Transmission
        var oilTransmissionConfig = new VehicleMaintenanceConfig
        {
            Name = "Oil Transmission Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Oil_Transmission:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Oil_Transmission:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Oil_Transmission:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Oil_Transmission:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(oilTransmissionConfig);
        await _reminderService.AddReminderAsync(oilTransmissionConfig, oilTransmissionConfig.Vehicle.VehicleInfo.Mileage);

        // Oil Differential
        var oilDifferentialConfig = new VehicleMaintenanceConfig
        {
            Name = "Oil Differential Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Oil_Differential:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Oil_Differential:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Oil_Differential:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Oil_Differential:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(oilDifferentialConfig);
        await _reminderService.AddReminderAsync(oilDifferentialConfig, oilDifferentialConfig.Vehicle.VehicleInfo.Mileage);

        // Liquid Coolant
        var liquidCoolantConfig = new VehicleMaintenanceConfig
        {
            Name = "Liquid Coolant Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Liquid_Coolant:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Liquid_Coolant:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Liquid_Coolant:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Liquid_Coolant:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(liquidCoolantConfig);
        await _reminderService.AddReminderAsync(liquidCoolantConfig, liquidCoolantConfig.Vehicle.VehicleInfo.Mileage);

        // Liquid Brake
        var liquidBrakeConfig = new VehicleMaintenanceConfig
        {
            Name = "Liquid Brake Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Liquid_Brake:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Liquid_Brake:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Liquid_Brake:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Liquid_Brake:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(liquidBrakeConfig);
        await _reminderService.AddReminderAsync(liquidBrakeConfig, liquidBrakeConfig.Vehicle.VehicleInfo.Mileage);

        // Liquid Servo Direction
        var liquidServoDirectionConfig = new VehicleMaintenanceConfig
        {
            Name = "Liquid Servo Direction Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Liquid_ServoDirection:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Liquid_ServoDirection:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Liquid_ServoDirection:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Liquid_ServoDirection:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(liquidServoDirectionConfig);
        await _reminderService.AddReminderAsync(liquidServoDirectionConfig, liquidServoDirectionConfig.Vehicle.VehicleInfo.Mileage);

        // Filter Air
        var filterAirConfig = new VehicleMaintenanceConfig
        {
            Name = "Air Filter Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Filter_Air:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Filter_Air:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Filter_Air:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Filter_Air:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(filterAirConfig);
        await _reminderService.AddReminderAsync(filterAirConfig, filterAirConfig.Vehicle.VehicleInfo.Mileage);

        // Filter Fuel
        var filterFuelConfig = new VehicleMaintenanceConfig
        {
            Name = "Fuel Filter Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Filter_Fuel:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Filter_Fuel:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Filter_Fuel:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Filter_Fuel:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(filterFuelConfig);
        await _reminderService.AddReminderAsync(filterFuelConfig, filterFuelConfig.Vehicle.VehicleInfo.Mileage);

        // Filter Cabin
        var filterCabinConfig = new VehicleMaintenanceConfig
        {
            Name = "Cabin Filter Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Filter_Cabin:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Filter_Cabin:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Filter_Cabin:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Filter_Cabin:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(filterCabinConfig);
        await _reminderService.AddReminderAsync(filterCabinConfig, filterCabinConfig.Vehicle.VehicleInfo.Mileage);

        // Brake Pads
        var brakePadsConfig = new VehicleMaintenanceConfig
        {
            Name = "Brake Pads Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Brake_Pads:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Brake_Pads:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Brake_Pads:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Brake_Pads:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(brakePadsConfig);
        await _reminderService.AddReminderAsync(brakePadsConfig, brakePadsConfig.Vehicle.VehicleInfo.Mileage);

        // Brake Discs
        var brakeDiscsConfig = new VehicleMaintenanceConfig
        {
            Name = "Brake Discs Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Brake_Discs:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Brake_Discs:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Brake_Discs:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Brake_Discs:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(brakeDiscsConfig);
        await _reminderService.AddReminderAsync(brakeDiscsConfig, brakeDiscsConfig.Vehicle.VehicleInfo.Mileage);

        // Eri
        var eriConfig = new VehicleMaintenanceConfig
        {
            Name = "Eri Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Eri:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Eri:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Eri:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Eri:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(eriConfig);
        await _reminderService.AddReminderAsync(eriConfig, eriConfig.Vehicle.VehicleInfo.Mileage);

        // Battery
        var batteryConfig = new VehicleMaintenanceConfig
        {
            Name = "Battery Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Battery:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Battery:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Battery:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Battery:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(batteryConfig);
        await _reminderService.AddReminderAsync(batteryConfig, batteryConfig.Vehicle.VehicleInfo.Mileage);

        // Direction
        var directionConfig = new VehicleMaintenanceConfig
        {
            Name = "Direction Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Direction:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Direction:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Direction:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Direction:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(directionConfig);
        await _reminderService.AddReminderAsync(directionConfig, directionConfig.Vehicle.VehicleInfo.Mileage);

        // Shock Absorbers
        var shockAbsorbersConfig = new VehicleMaintenanceConfig
        {
            Name = "Shock Absorbers Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Shock_Absorbers:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Shock_Absorbers:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Shock_Absorbers:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Shock_Absorbers:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(shockAbsorbersConfig);
        await _reminderService.AddReminderAsync(shockAbsorbersConfig, shockAbsorbersConfig.Vehicle.VehicleInfo.Mileage);

        // Clutch
        var clutchConfig = new VehicleMaintenanceConfig
        {
            Name = "Clutch Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Clutch:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Clutch:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Clutch:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Clutch:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(clutchConfig);
        await _reminderService.AddReminderAsync(clutchConfig, clutchConfig.Vehicle.VehicleInfo.Mileage);

        // Differential
        var differentialConfig = new VehicleMaintenanceConfig
        {
            Name = "Differential Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Differential:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Differential:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Differential:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Differential:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(differentialConfig);
        await _reminderService.AddReminderAsync(differentialConfig, differentialConfig.Vehicle.VehicleInfo.Mileage);

        // Freon Air Conditioning
        var freonAirConditioningConfig = new VehicleMaintenanceConfig
        {
            Name = "Freon Air Conditioning Change",
            DateIntervalConfig = int.Parse(maintenanceSettings["Freon_Air_Conditioning:Time"]),
            MileageIntervalConfig = int.Parse(maintenanceSettings["Freon_Air_Conditioning:Mileage"]),
            IsEditable = true,
            MaintenanceTypeId = int.Parse(maintenanceSettings["Freon_Air_Conditioning:TypeId"]),
            MaintenanceCategoryId = int.Parse(maintenanceSettings["Freon_Air_Conditioning:CategoryId"]),
            VehicleId = vehicleId
        };
        await AddConfigAsync(freonAirConditioningConfig);
        await _reminderService.AddReminderAsync(freonAirConditioningConfig, freonAirConditioningConfig.Vehicle.VehicleInfo.Mileage);
    }


    private async Task AddConfigAsync(VehicleMaintenanceConfig vehicleMaintenanceConfig)
    {
        await _repository.AddAsync(vehicleMaintenanceConfig);
    }
}