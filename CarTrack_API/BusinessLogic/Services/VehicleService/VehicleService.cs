using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.BusinessLogic.Services.NotificationService;
using CarTrack_API.BusinessLogic.Services.ReminderService;
using CarTrack_API.BusinessLogic.Services.VehicleMaintenanceConfigService;
using CarTrack_API.DataAccess.Repositories.VehicleMaintenanceConfigRepository;
using CarTrack_API.DataAccess.Repositories.VehicleRepository;
using CarTrack_API.EntityLayer.Dtos.BodyDto;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Dtos.Usage;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using CarTrack_API.EntityLayer.Dtos.VehicleEngineDto;
using CarTrack_API.EntityLayer.Dtos.VehicleInfo;
using CarTrack_API.EntityLayer.Dtos.VehicleModelDto;
using CarTrack_API.EntityLayer.Dtos.VehicleUsageStatsDto;
using CarTrack_API.EntityLayer.Exceptions.VehicleException;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.VehicleService;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IVehicleMaintenanceConfigService _vehicleConfigService;
    private readonly IReminderService _reminderService;
    private readonly INotificationService _notificationService;
    private readonly IVehicleMaintenanceConfigRepository _configRepository;

    public VehicleService(
        IVehicleRepository vehicleRepository,
        IVehicleMaintenanceConfigService vehicleConfigService,
        IReminderService reminderService,
        IVehicleMaintenanceConfigRepository configRepository,
        INotificationService notificationService)
    {
        _vehicleRepository = vehicleRepository;
        _vehicleConfigService = vehicleConfigService;
        _reminderService = reminderService;
        _configRepository = configRepository;
        _notificationService = notificationService;
    }

    private async Task<VehicleInfo> UpdateOdometerAndInfoAsync(int vehicleId, double newOdometerValue, DateTime readingDate, string source)
    {
        var readingDateUtc = DateTime.SpecifyKind(readingDate, DateTimeKind.Utc);

        var previousReading = await _vehicleRepository.GetLastReadingBeforeDateAsync(vehicleId, readingDateUtc);
        if (previousReading != null && newOdometerValue < previousReading.OdometerValue)
        {
            throw new ArgumentException($"Mileage ({newOdometerValue}) on {readingDate.ToShortDateString()} cannot be less than a previous reading ({previousReading.OdometerValue} on {previousReading.ReadingDate.ToShortDateString()}).");
        }

        var nextReading = await _vehicleRepository.GetFirstReadingAfterDateAsync(vehicleId, readingDateUtc);
        if (nextReading != null && newOdometerValue > nextReading.OdometerValue)
        {
            throw new ArgumentException($"Mileage ({newOdometerValue}) on {readingDate.ToShortDateString()} cannot be greater than a future reading ({nextReading.OdometerValue} on {nextReading.ReadingDate.ToShortDateString()}).");
        }

        var vehicleInfo = await _vehicleRepository.GetInfoByVehicleIdForUpdateAsync(vehicleId);
        if (vehicleInfo == null)
        {
            throw new VehicleNotFoundException($"Vehicle info not found for vehicle ID {vehicleId}.");
        }

        _vehicleRepository.AddMileageReading(new MileageReading { VehicleId = vehicleId, OdometerValue = newOdometerValue, ReadingDate = readingDateUtc, Source = source });

        if (newOdometerValue > vehicleInfo.Mileage)
        {
            vehicleInfo.Mileage = newOdometerValue;
        }
        vehicleInfo.LastUpdate = DateTime.UtcNow;

        return vehicleInfo;
    }

    private async Task RecalculateAverageDailyDriveAsync(VehicleInfo vehicleInfo)
    {
        var readings = await _vehicleRepository.GetMileageReadingsForDateRangeAsync(vehicleInfo.VehicleId, DateTime.UtcNow.AddDays(-365));
        if (readings.Count < 2) return;

        var firstReading = readings.First();
        var lastReading = readings.Last();
        var totalDistance = lastReading.OdometerValue - firstReading.OdometerValue;
        var totalDays = (lastReading.ReadingDate - firstReading.ReadingDate).TotalDays;

        if (totalDistance <= 0 || totalDays < 7) return;

        var newAverage = Math.Round(totalDistance / totalDays, 2);

        if (newAverage > 0 && newAverage < 1000)
        {
            vehicleInfo.AverageTravelDistance = newAverage;
        }
    }

    public async Task AddMileageReadingAsync(int vehicleId, AddMileageReadingRequestDto request)
    {
        var vehicleInfo = await UpdateOdometerAndInfoAsync(vehicleId, request.OdometerValue, DateTime.UtcNow, "QuickSync");
        await RecalculateAverageDailyDriveAsync(vehicleInfo);
        await _vehicleRepository.SaveChangesAsync();
    }

    public async Task AddVehicleMaintenanceAsync(VehicleMaintenanceRequestDto request)
    {
        var vehicleInfo = await UpdateOdometerAndInfoAsync(request.VehicleId, request.Mileage, request.Date, "MaintenanceLog");
        _vehicleRepository.AddVehicleMaintenance(request.ToMaintenanceUnverifiedRecord());
        await RecalculateAverageDailyDriveAsync(vehicleInfo);

        await _vehicleRepository.SaveChangesAsync();

        await _reminderService.UpdateReminderAsync(request);
    }

    public async Task AddVehicleAsync(VehicleRequestDto vehicleRequestDto)
    {
        var existingVehicle = await _vehicleRepository.GetVehicleForValidationAsync(vehicleRequestDto.Vin);
        if (existingVehicle != null)
        {
            throw new VehicleAlreadyExistException($"An active vehicle with VIN {vehicleRequestDto.Vin} already exists!");
        }

        var vehicle = vehicleRequestDto.ToVehicle();
        _vehicleRepository.AddVehicle(vehicle);
        await _vehicleRepository.SaveChangesAsync();

        var fullVehicle = await _vehicleRepository.GetVehicleWithDetailsByIdAsync(vehicle.Id);
        if (fullVehicle != null)
        {
            await _vehicleConfigService.DefaultMaintenanceConfigAsync(fullVehicle);
        }
    }

    public async Task DeactivateVehicleAsync(int vehicleId)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
        if (vehicle == null) return;

        vehicle.IsActive = false;
        _vehicleRepository.Update(vehicle);
        await _vehicleRepository.SaveChangesAsync();

        await _reminderService.SoftDeleteRemindersForVehicleAsync(vehicleId);
        await _notificationService.DeactivateNotificationsForVehicleAsync(vehicleId);
    }

    public async Task AddCustomReminderAsync(int vehicleId, CustomReminderRequestDto request)
    {
        if (request.MileageInterval < 0 && request.DateInterval < 0)
        {
            throw new ArgumentException("At least one interval (mileage or date) must be provided.");
        }
        if (await _configRepository.DoesConfigNameExistForVehicleAsync(vehicleId, request.Name))
        {
            throw new ArgumentException($"A reminder with the name '{request.Name}' already exists for this vehicle.");
        }

        var newConfig = new VehicleMaintenanceConfig { VehicleId = vehicleId, Name = request.Name, MaintenanceTypeId = request.MaintenanceTypeId, MileageIntervalConfig = request.MileageInterval, DateIntervalConfig = request.DateInterval, IsEditable = true, IsCustom = true };
        await _configRepository.AddAsync(newConfig);

        var vehicleInfo = await _vehicleRepository.GetInfoByVehicleIdAsync(vehicleId);
        if (vehicleInfo == null)
        {
            throw new InvalidOperationException("Cannot add reminder, vehicle info not found.");
        }

        await _reminderService.AddReminderAsync(newConfig, vehicleInfo.Mileage);
    }

    public async Task DeleteCustomReminderAsync(int configId)
    {
        var configToDelete = await _configRepository.GetByIdAsync(configId);
        if (configToDelete == null) return;

        if (!configToDelete.IsCustom)
        {
            throw new InvalidOperationException("Default reminders cannot be deleted. They can only be deactivated.");
        }
        await _configRepository.DeleteAsync(configToDelete);
    }

    public async Task<bool> UserOwnsVehicleAsync(int userId, int vehicleId)
    {
        return await _vehicleRepository.DoesUserOwnVehicleAsync(userId, vehicleId);
    }

    public async Task<List<VehicleResponseDto>> GetAllByClientIdAsync(int clientId)
    {
        var vehicles = await _vehicleRepository.GetVehiclesForListViewAsync(clientId);
        return vehicles.ToListVehicleResponseDto();
    }

    public async Task<VehicleEngineResponseDto?> GetVehicleEngineByVehicleIdAsync(int vehicleId)
    {
        var engine = await _vehicleRepository.GetEngineByVehicleIdAsync(vehicleId);
        return engine?.ToVehicleEngineResponseDto();
    }

    public async Task<VehicleModelResponseDto?> GetVehicleModelByVehicleIdAsync(int vehicleId)
    {
        var model = await _vehicleRepository.GetModelByVehicleIdAsync(vehicleId);
        return model?.ToVehicleModelResponseDto();
    }

    public async Task<VehicleInfoResponseDto?> GetVehicleInfoByVehicleIdAsync(int vehicleId)
    {
        var info = await _vehicleRepository.GetInfoByVehicleIdAsync(vehicleId);
        return info?.ToVehicleInfoResponseDto();
    }

    public async Task<BodyResponseDto?> GetBodyByVehicleIdAsync(int vehicleId)
    {
        var body = await _vehicleRepository.GetBodyByVehicleIdAsync(vehicleId);
        return body?.ToBodyResponseDto();
    }

    public async Task<List<MaintenanceLogDto>> GetMaintenanceHistoryAsync(int vehicleId)
    {
        var maintenanceLogs = await _vehicleRepository.GetMaintenanceHistoryByVehicleIdAsync(vehicleId);
        if (!maintenanceLogs.Any()) return new List<MaintenanceLogDto>();

        var configIds = maintenanceLogs
            .Where(log => log.RelatedConfigIds != null && log.RelatedConfigIds.Any())
            .SelectMany(log => log.RelatedConfigIds)
            .Distinct()
            .ToList();

        var configIdToNameMap = new Dictionary<int, string>();
        if (configIds.Any())
        {
            configIdToNameMap = await _vehicleRepository.GetMaintenanceConfigNamesByIds(configIds);
        }
        return maintenanceLogs.ToMaintenanceLogDtoList(configIdToNameMap);
    }

    public async Task<List<DailyUsageDto>> GetDailyUsageForLastWeekAsync(int vehicleId, string timeZoneId)
    {
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        var todayInZone = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone).Date;
        var firstDayOfInterval = todayInZone.AddDays(-6);
        var startDateUtc = TimeZoneInfo.ConvertTimeToUtc(firstDayOfInterval.AddDays(-1), timeZone);
        var readings = await _vehicleRepository.GetMileageReadingsForDateRangeAsync(vehicleId, startDateUtc);

        if (!readings.Any())
        {
            return Enumerable.Range(0, 7)
                .Select(i => new DailyUsageDto { DayLabel = firstDayOfInterval.AddDays(i).ToString("ddd", new System.Globalization.CultureInfo("en-US")), Distance = 0 })
                .ToList();
        }

        var results = new List<DailyUsageDto>();
        double lastKnownMileage = readings.First().OdometerValue;

        for (int i = 0; i < 7; i++)
        {
            var currentDate = firstDayOfInterval.AddDays(i);
            var readingsOfCurrentDay = readings
                .Where(r => TimeZoneInfo.ConvertTimeFromUtc(r.ReadingDate, timeZone).Date == currentDate)
                .ToList();

            double dailyDistance = 0;
            if (readingsOfCurrentDay.Any())
            {
                var dayEndMileage = readingsOfCurrentDay.Max(r => r.OdometerValue);
                dailyDistance = dayEndMileage - lastKnownMileage;
                lastKnownMileage = dayEndMileage;
            }
            results.Add(new DailyUsageDto { DayLabel = currentDate.ToString("ddd", new System.Globalization.CultureInfo("en-US")), Distance = Math.Max(0, dailyDistance) });
        }
        return results;
    }
}