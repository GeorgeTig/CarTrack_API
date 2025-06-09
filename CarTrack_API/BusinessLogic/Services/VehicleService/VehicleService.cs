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

        //=====================================================================
        // METODA CENTRALĂ PRIVATĂ PENTRU GESTIONAREA ODOMETRULUI
        //=====================================================================
        private async Task UpdateOdometerAsync(int vehicleId, double newOdometerValue, DateTime readingDate, string source)
        {
            // Pasul 1: Validare cronologică
            var previousReading = await _vehicleRepository.GetLastReadingBeforeDateAsync(vehicleId, readingDate);
            if (previousReading != null && newOdometerValue < previousReading.OdometerValue)
            {
                throw new ArgumentException($"Mileage ({newOdometerValue}) cannot be less than a previous reading ({previousReading.OdometerValue} on {previousReading.ReadingDate.ToShortDateString()}).");
            }

            var nextReading = await _vehicleRepository.GetFirstReadingAfterDateAsync(vehicleId, readingDate);
            if (nextReading != null && newOdometerValue > nextReading.OdometerValue)
            {
                throw new ArgumentException($"Mileage ({newOdometerValue}) cannot be greater than a future reading ({nextReading.OdometerValue} on {nextReading.ReadingDate.ToShortDateString()}).");
            }

            // Pasul 2: Adăugare în istoric
            var newReading = new MileageReading
            {
                VehicleId = vehicleId,
                OdometerValue = newOdometerValue,
                ReadingDate = readingDate,
                Source = source
            };
            // Nota: Repository-ul de vehicule se va ocupa de salvare
            await _vehicleRepository.AddMileageReadingAsync(newReading); 

            // Pasul 3: Actualizarea valorii maxime în VehicleInfo
            var vehicleInfo = await _vehicleRepository.GetInfoByVehicleIdAsync(vehicleId);
            if (vehicleInfo != null)
            {
                if (newOdometerValue > vehicleInfo.Mileage)
                {
                    vehicleInfo.Mileage = newOdometerValue;
                }
                vehicleInfo.LastUpdate = DateTime.UtcNow;
                await _vehicleRepository.UpdateVehicleInfoAsync(vehicleInfo);
            }
        }

        //=====================================================================
        // METODE PUBLICE REFACTORIZATE
        //=====================================================================

        public async Task AddMileageReadingAsync(int vehicleId, AddMileageReadingRequestDto request)
        {
            await UpdateOdometerAsync(vehicleId, request.OdometerValue, DateTime.UtcNow, "QuickSync");
        }

        public async Task AddVehicleMaintenanceAsync(VehicleMaintenanceRequestDto request)
        {
            // Pasul 1: Gestionează actualizarea odometrului (va arunca excepție dacă e invalid)
            await UpdateOdometerAsync(request.VehicleId, request.Mileage, request.Date, "MaintenanceLog");

            // Pasul 2: Adaugă înregistrarea de mentenanță
            var maintenanceRecord = request.ToMaintenanceUnverifiedRecord();
            await _vehicleRepository.AddVehicleMaintenanceAsync(maintenanceRecord);
            
            // Pasul 3: Actualizează reminderele corespunzătoare
            await _reminderService.UpdateReminderAsync(request);
        }

        public async Task AddVehicleAsync(VehicleRequestDto vehicleRequestDto)
        {
            var vehicle = vehicleRequestDto.ToVehicle();
            await _vehicleRepository.AddVehicleAsync(vehicle);

            // Presupunând că ai implementat GetVehicleWithDetailsByIdAsync în repository
            var fullVehicle = await _vehicleRepository.GetVehicleWithDetailsByIdAsync(vehicle.Id);
            if (fullVehicle != null)
            {
                 // Apelează serviciul care generează planul de mentenanță (fie el static sau dinamic)
                await _vehicleConfigService.DefaultMaintenanceConfigAsync(fullVehicle);
            }
        }

        //=====================================================================
        // METODE PUBLICE NEMODIFICATE (EXISTENTE)
        //=====================================================================

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

        public async Task<List<MaintenanceLogDto>> GetMaintenanceHistoryAsync(int vehicleId)
        {
            var maintenanceLogs = await _vehicleRepository.GetMaintenanceHistoryByVehicleIdAsync(vehicleId);
            return maintenanceLogs.ToMaintenanceLogDtoList();
        }

        public async Task<List<DailyUsageDto>> GetDailyUsageForLastWeekAsync(int vehicleId, string timeZoneId)
        {
            // Logica ta existentă (pe care am discutat că o putem îmbunătăți ulterior)
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
