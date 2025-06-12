using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.BusinessLogic.Services.ReminderService;
using CarTrack_API.BusinessLogic.Services.VehicleMaintenanceConfigService;
using CarTrack_API.DataAccess.Repositories.VehicleRepository;
using CarTrack_API.EntityLayer.Dtos.BodyDto;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
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

    public VehicleService(
        IVehicleRepository vehicleRepository,
        IVehicleMaintenanceConfigService vehicleConfigService,
        IReminderService reminderService)
    {
        _vehicleRepository = vehicleRepository;
        _vehicleConfigService = vehicleConfigService;
        _reminderService = reminderService;
    }
    
    public async Task<bool> UserOwnsVehicleAsync(int userId, int vehicleId)
    {
        return await _vehicleRepository.DoesUserOwnVehicleAsync(userId, vehicleId);
    }
    
    // Metodă privată centrală pentru actualizarea odometrului și a mediei zilnice
   private async Task UpdateOdometerAsync(int vehicleId, double newOdometerValue, DateTime readingDate, string source)
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
        
        // 2. Adaugă noua citire în istoric.
        var newReading = new MileageReading
        {
            VehicleId = vehicleId,
            OdometerValue = newOdometerValue,
            ReadingDate = readingDateUtc,
            Source = source
        };
        await _vehicleRepository.AddMileageReadingAsync(newReading);
        
        // 3. Preluăm VehicleInfo pentru a-l actualiza.
        var vehicleInfo = await _vehicleRepository.GetInfoByVehicleIdAsync(vehicleId);
        if (vehicleInfo == null)
        {
             // Măsură de siguranță, acest lucru nu ar trebui să se întâmple pentru un vehicul existent.
            throw new VehicleNotFoundException($"Vehicle info not found for vehicle ID {vehicleId}.");
        }
        
        // 4. Actualizăm kilometrajul maxim dacă noua valoare este mai mare.
        // Aceasta este implementarea Scenariului 2.
        if (newOdometerValue > vehicleInfo.Mileage)
        {
            vehicleInfo.Mileage = newOdometerValue;
        }
        vehicleInfo.LastUpdate = DateTime.UtcNow;

        // Important: Salvăm toate modificările de până acum într-o singură tranzacție.
        // Adăugarea MileageReading și actualizarea VehicleInfo sunt acum atomice.
        await _vehicleRepository.SaveChangesAsync();

        // 5. Recalculăm media zilnică, acum că datele noi sunt salvate.
        await RecalculateAverageDailyDriveAsync(vehicleId);
    }

    // Metodă privată nouă pentru calculul mediei
    private async Task RecalculateAverageDailyDriveAsync(int vehicleId)
    {
        var relevantReadings = await _vehicleRepository.GetMileageReadingsForDateRangeAsync(vehicleId, DateTime.UtcNow.AddDays(-365));
        if (relevantReadings.Count < 2) return;

        var firstReading = relevantReadings.First();
        var lastReading = relevantReadings.Last();
        var totalDistance = lastReading.OdometerValue - firstReading.OdometerValue;
        var totalDays = (lastReading.ReadingDate - firstReading.ReadingDate).TotalDays;

        if (totalDistance <= 0 || totalDays < 7) return;

        var newAverage = totalDistance / totalDays;
        var vehicleInfo = await _vehicleRepository.GetInfoByVehicleIdAsync(vehicleId);

        if (vehicleInfo != null)
        {
            if (newAverage > 0 && newAverage < 1000)
            {
                vehicleInfo.AverageTravelDistance = Math.Round(newAverage, 2);
                await _vehicleRepository.UpdateVehicleInfoAsync(vehicleInfo);
            }
        }
    }

    // --- Metode publice ale serviciului ---

    public async Task AddMileageReadingAsync(int vehicleId, AddMileageReadingRequestDto request)
    {
        await UpdateOdometerAsync(vehicleId, request.OdometerValue, DateTime.UtcNow, "QuickSync");
    }

    public async Task AddVehicleMaintenanceAsync(VehicleMaintenanceRequestDto request)
    {
        await UpdateOdometerAsync(request.VehicleId, request.Mileage, request.Date, "MaintenanceLog");

        var maintenanceRecord = request.ToMaintenanceUnverifiedRecord();
        await _vehicleRepository.AddVehicleMaintenanceAsync(maintenanceRecord);
        
        await _reminderService.UpdateReminderAsync(request);
    }

    public async Task AddVehicleAsync(VehicleRequestDto vehicleRequestDto)
    {
        var vehicle = vehicleRequestDto.ToVehicle();
        await _vehicleRepository.AddVehicleAsync(vehicle);

        var fullVehicle = await _vehicleRepository.GetVehicleWithDetailsByIdAsync(vehicle.Id);
        if (fullVehicle != null)
        {
            await _vehicleConfigService.DefaultMaintenanceConfigAsync(fullVehicle);
        }
    }

    public async Task<List<MaintenanceLogDto>> GetMaintenanceHistoryAsync(int vehicleId)
    {
        var maintenanceLogs = await _vehicleRepository.GetMaintenanceHistoryByVehicleIdAsync(vehicleId);
        if (!maintenanceLogs.Any())
        {
            return new List<MaintenanceLogDto>();
        }
        
        var allConfigIds = maintenanceLogs
            .Where(log => log.RelatedConfigIds != null && log.RelatedConfigIds.Any())
            .SelectMany(log => log.RelatedConfigIds)
            .Distinct()
            .ToList();

        var configIdToNameMap = new Dictionary<int, string>();
        if (allConfigIds.Any())
        {
            configIdToNameMap = await _vehicleRepository.GetMaintenanceConfigNamesByIds(allConfigIds);
        }

        return maintenanceLogs.ToMaintenanceLogDtoList(configIdToNameMap);
    }

    public async Task<List<VehicleResponseDto>> GetAllByClientIdAsync(int id)
    {
        var vehicles = await _vehicleRepository.GetVehiclesForListViewAsync(id);
        return vehicles.ToListVehicleResponseDto();
    }

    public async Task<VehicleEngineResponseDto> GetVehicleEngineByVehicleIdAsync(int vehicleId)
    {
        var vehicleEngine = await _vehicleRepository.GetEngineByVehicleIdAsync(vehicleId);
        if (vehicleEngine == null) throw new VehicleNotFoundException("Vehicle Engine not found");
        return vehicleEngine.ToVehicleEngineResponseDto();
    }

    public async Task<VehicleModelResponseDto> GetVehicleModelByVehicleIdAsync(int vehicleId)
    {
        var vehicleModel = await _vehicleRepository.GetModelByVehicleIdAsync(vehicleId);
        if (vehicleModel == null) throw new VehicleNotFoundException("Vehicle Model not found");
        return vehicleModel.ToVehicleModelResponseDto();
    }

    public async Task<VehicleInfoResponseDto> GetVehicleInfoByVehicleIdAsync(int vehicleId)
    {
        var vehicleInfo = await _vehicleRepository.GetInfoByVehicleIdAsync(vehicleId);
        if (vehicleInfo == null) throw new VehicleNotFoundException("Vehicle Info not found");
        return vehicleInfo.ToVehicleInfoResponseDto();
    }

    public async Task<BodyResponseDto> GetVehicleBodyByVehicleIdAsync(int vehicleId)
    {
        var body = await _vehicleRepository.GetBodyByVehicleIdAsync(vehicleId);
        if (body == null) throw new VehicleNotFoundException("Vehicle Body not found");
        return body.ToBodyResponseDto();
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
            results.Add(new DailyUsageDto
            {
                DayLabel = currentDate.ToString("ddd", new System.Globalization.CultureInfo("en-US")),
                Distance = Math.Max(0, dailyDistance)
            });
        }
        return results;
    }
}