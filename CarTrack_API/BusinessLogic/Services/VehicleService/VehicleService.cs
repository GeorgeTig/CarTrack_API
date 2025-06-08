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

public class VehicleService(IVehicleRepository vehicleRepository, IVehicleMaintenanceConfigService vehicleConfigService) : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
    private readonly IVehicleMaintenanceConfigService _vehicleConfigService = vehicleConfigService;
    
    public async Task<List<VehicleResponseDto>> GetAllByClientIdAsync(int id)
    {
        var vehicles = await _vehicleRepository.GetVehiclesForListViewAsync(id);
        return vehicles.ToListVehicleResponseDto();
    }

    public async Task AddVehicleAsync(VehicleRequestDto vehicleRequestDto)
    {
        var vehicle = vehicleRequestDto.ToVehicle(); 

        // 2. Îl adaugi în baza de date
        await _vehicleRepository.AddVehicleAsync(vehicle);
        // După acest pas, vehicle.Id este populat, dar vehicle.VehicleModel este probabil null.

        // --- AICI ESTE PASUL CRUCIAL LIPSĂ ---
        // Înainte de a apela serviciul de configurare, trebuie să re-încărcăm obiectul
        // 'vehicle' cu toate relațiile de care are nevoie calculatorul.

        var fullVehicle = await _vehicleRepository.GetVehicleWithDetailsByIdAsync(vehicle.Id); // Trebuie să creezi această metodă în repository

        // 3. Apelezi serviciul de configurare cu obiectul complet
        await _vehicleConfigService.DefaultMaintenanceConfigAsync(fullVehicle); 
    }
    
    public async Task AddVehicleMaintenanceAsync(VehicleMaintenanceRequestDto request)
    {
        var maintenanceRecord = request.ToMaintenanceUnverifiedRecord();
        await _vehicleRepository.AddVehicleMaintenanceAsync(maintenanceRecord);
        
        // Când se adaugă o mentenanță, creăm și o citire de kilometraj
        var mileageReading = new MileageReading
        {
            VehicleId = request.VehicleId,
            OdometerValue = request.Mileage,
            ReadingDate = request.Date.ToUniversalTime(),
            Source = "MaintenanceLog"
        };
        await AddMileageReadingAsync(request.VehicleId, new AddMileageReadingRequestDto { OdometerValue = request.Mileage });
    }
    
    public async Task AddMileageReadingAsync(int vehicleId, AddMileageReadingRequestDto request)
    {
        var lastReading = await _vehicleRepository.GetLastMileageReadingAsync(vehicleId);
        if (lastReading != null && request.OdometerValue < lastReading.OdometerValue)
        {
            throw new ArgumentException($"New mileage ({request.OdometerValue}) cannot be less than the last recorded mileage ({lastReading.OdometerValue}).");
        }

        var newReading = new MileageReading
        {
            VehicleId = vehicleId,
            OdometerValue = request.OdometerValue,
            ReadingDate = DateTime.UtcNow,
            Source = "QuickSync"
        };
        
        await _vehicleRepository.AddMileageReadingAsync(newReading);
        
        var vehicleInfo = await _vehicleRepository.GetInfoByVehicleIdAsync(vehicleId);
        if (vehicleInfo != null)
        {
            vehicleInfo.Mileage = request.OdometerValue;
            vehicleInfo.LastUpdate = newReading.ReadingDate;
            await _vehicleRepository.UpdateVehicleInfoAsync(vehicleInfo);
        }
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
    
    public async Task<List<MaintenanceLogDto>> GetMaintenanceHistoryAsync(int vehicleId)
    {
        var maintenanceLogs = await _vehicleRepository.GetMaintenanceHistoryByVehicleIdAsync(vehicleId);
        return maintenanceLogs.ToMaintenanceLogDtoList();
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