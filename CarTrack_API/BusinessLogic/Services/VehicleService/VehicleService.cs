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

public class VehicleService(IVehicleRepository vehicleRepository, IVehicleMaintenanceConfigService vehicleConfigService)
    : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
    private readonly IVehicleMaintenanceConfigService _vehicleConfigService = vehicleConfigService;

    public async Task<List<VehicleResponseDto>> GetAllByClientIdAsync(int id)
    {
        var vehicles = await _vehicleRepository.GetAllByClientIdAsync(id);
        var vehicleResponseDtos = vehicles.ToListVehicleResponseDto();
        return vehicleResponseDtos;
    }

    public async Task AddVehicleAsync(VehicleRequestDto vehicleRequestDto)
    {
        try
        {
            var vehicle = vehicleRequestDto.ToVehicle();
            await _vehicleRepository.AddVehicleAsync(vehicle);
            await _vehicleConfigService.DefaultMaintenanceConfigAsync(vehicle.Id);
        }
        catch (VehicleAlreadyExistException)
        {
            throw;
        }
    }

    public async Task<VehicleEngineResponseDto> GetVehicleEngineByVehicleIdAsync(int vehId)
    {
        var vehicle = await _vehicleRepository.GetVehicleEngineByVehicleIdAsync(vehId);
        if (vehicle == null)
        {
            throw new VehicleNotFoundException("VehicleEngine not found");
        }

        var vehicleEngineResponseDto = vehicle.ToVehicleEngineResponseDto();

        return vehicleEngineResponseDto;
    }

    public async Task<VehicleModelResponseDto> GetVehicleModelByVehicleIdAsync(int vehId)
    {
        var vehicle = await _vehicleRepository.GetVehicleModelByVehicleIdAsync(vehId);
        if (vehicle == null)
        {
            throw new VehicleNotFoundException("VehicleModel not found");
        }

        var vehicleModelResponseDto = vehicle.ToVehicleModelResponseDto();

        return vehicleModelResponseDto;
    }

    public async Task<VehicleInfoResponseDto> GetVehicleInfoByVehicleIdAsync(int vehId)
    {
        var vehicle = await _vehicleRepository.GetVehicleInfoByVehicleIdAsync(vehId);
        if (vehicle == null)
        {
            throw new VehicleNotFoundException("VehicleInfo not found");
        }

        var vehicleInfoResponseDto = vehicle.ToVehicleInfoResponseDto();

        return vehicleInfoResponseDto;
    }

    public async Task<BodyResponseDto> GetVehicleBodyByVehicleIdAsync(int vehId)
    {
        var vehicle = await _vehicleRepository.GetVehicleBodyByVehicleIdAsync(vehId);
        if (vehicle == null)
        {
            throw new VehicleNotFoundException("Vehicle not found");
        }

        var vehicleBody = vehicle.ToBodyResponseDto();

        return vehicleBody;
    }

    public async Task AddVehicleMaintenanceAsync(VehicleMaintenanceRequestDto vehRequest)
    {
        var maintenance = MappingMaintenanceRecord.ToMaintenanceUnverifiedRecord(vehRequest);
        await _vehicleRepository.AddVehicleMaintenanceAsync(maintenance);
    }

    public async Task<List<MaintenanceLogDto>> GetMaintenanceHistoryAsync(int vehId)
    {
        var maintenanceLog = await _vehicleRepository.GetVehicleMaintenanceByVehicleIdAsync(vehId);
        if (maintenanceLog == null)
        {
            throw new VehicleNotFoundException("Vehicle maintenance log not found");
        }

        var maintenanceLogDto = maintenanceLog.ToMaintenanceLogDtoList();

        return maintenanceLogDto;
    }

    public async Task<List<DailyUsageDto>> GetDailyUsageForLastWeekAsync(int vehicleId, string timeZoneId)
    {
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        var todayInZone = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone).Date;
        var firstDayOfInterval = todayInZone.AddDays(-6);

        // --- AICI ESTE CORECȚIA CRUCIALĂ PENTRU EROAREA DE DATETIME ---
        // Convertim data locală înapoi la UTC înainte de a o trimite la baza de date.
        // Presupunem că datele din DB sunt stocate în UTC.
        var startDateUtc = TimeZoneInfo.ConvertTimeToUtc(firstDayOfInterval, timeZone);

        var readings = await _vehicleRepository.GetMileageReadingsForDateRangeAsync(vehicleId, startDateUtc);

        if (!readings.Any())
        {
            return Enumerable.Range(0, 7)
                .Select(i => new DailyUsageDto { DayLabel = firstDayOfInterval.AddDays(i).ToString("ddd"), Distance = 0 })
                .ToList();
        }
    
        var results = new List<DailyUsageDto>();
        for (int i = 0; i < 7; i++)
        {
            var currentDate = firstDayOfInterval.AddDays(i);
            var lastReadingOfCurrentDay = readings.LastOrDefault(r => TimeZoneInfo.ConvertTimeFromUtc(r.ReadingDate, timeZone).Date == currentDate);
            var lastReadingOfPreviousDay = readings.LastOrDefault(r => TimeZoneInfo.ConvertTimeFromUtc(r.ReadingDate, timeZone).Date < currentDate);
        
            double dailyDistance = 0;
            if (lastReadingOfCurrentDay != null)
            {
                double startMileage = lastReadingOfPreviousDay?.OdometerValue ?? lastReadingOfCurrentDay.OdometerValue;
                dailyDistance = lastReadingOfCurrentDay.OdometerValue - startMileage;
            }
            results.Add(new DailyUsageDto { DayLabel = currentDate.ToString("ddd"), Distance = Math.Max(0, dailyDistance) });
        }
        return results;
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
    
        // Pasul 1: Salvează noua citire a kilometrajului
        await _vehicleRepository.AddMileageReadingAsync(newReading);
    
        // Pasul 2: Actualizează kilometrajul principal și data de update din VehicleInfo
        var vehicleInfo = await _vehicleRepository.GetVehicleInfoByVehicleIdAsync(vehicleId);
        if (vehicleInfo != null)
        {
            vehicleInfo.Mileage = request.OdometerValue;
            vehicleInfo.LastUpdate = newReading.ReadingDate;
        
            // --- APEL CORECT CĂTRE REPOSITORY ---
            await _vehicleRepository.UpdateVehicleInfoAsync(vehicleInfo);
        }
        else
        {
            // Acest caz nu ar trebui să se întâmple dacă vehiculul există, dar e bine să-l avem
            throw new VehicleNotFoundException($"VehicleInfo for VehicleId {vehicleId} not found.");
        }
    }
}