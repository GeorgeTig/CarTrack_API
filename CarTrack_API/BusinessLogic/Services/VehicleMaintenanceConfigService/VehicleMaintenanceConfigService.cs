using CarTrack_API.BusinessLogic.Services.MaintenanceCalculatorService;
using CarTrack_API.BusinessLogic.Services.ReminderService;
using CarTrack_API.DataAccess.Repositories.VehicleMaintenanceConfigRepository;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.VehicleMaintenanceConfigService;

 public class VehicleMaintenanceConfigService : IVehicleMaintenanceConfigService
    {
        private readonly IVehicleMaintenanceConfigRepository _configRepository;
        private readonly IMaintenanceCalculatorService _calculatorService;
        private readonly IReminderService _reminderService;
        private readonly ILogger<VehicleMaintenanceConfigService> _logger;

        public VehicleMaintenanceConfigService(
            IVehicleMaintenanceConfigRepository configRepository,
            IMaintenanceCalculatorService calculatorService,
            IReminderService reminderService,
            ILogger<VehicleMaintenanceConfigService> logger)
        {
            _configRepository = configRepository;
            _calculatorService = calculatorService;
            _reminderService = reminderService;
            _logger = logger;
        }

        public async Task DefaultMaintenanceConfigAsync(Vehicle vehicle)
        {
            _logger.LogInformation("Se generează planul de mentenanță pentru vehiculul cu ID: {VehicleId}", vehicle.Id);
            
            if (vehicle.VehicleModel?.VehicleEngine == null || vehicle.VehicleInfo == null)
            {
                _logger.LogError("Nu se poate genera planul de mentenanță pentru vehiculul ID {VehicleId} deoarece lipsesc informații esențiale (Model, Engine sau Info).", vehicle.Id);
                return;
            }

            var calculatedPlan = _calculatorService.GeneratePlanForVehicle(vehicle);
            _logger.LogInformation("Calculatorul de mentenanță a generat {Count} elemente pentru plan.", calculatedPlan.Count);

            // Pasul 2: Se iterează prin fiecare element al planului calculat
            foreach (var planItem in calculatedPlan)
            {
                // Se creează un nou obiect de configurare
                var newConfig = new VehicleMaintenanceConfig
                {
                    VehicleId = vehicle.Id,
                    Name = planItem.Name,
                    MaintenanceTypeId = planItem.TypeId,
                    MileageIntervalConfig = planItem.MileageInterval,
                    DateIntervalConfig = planItem.TimeInterval,
                    IsEditable = true
                };

                await _configRepository.AddAsync(newConfig);
                await _reminderService.AddReminderAsync(newConfig, vehicle.VehicleInfo.Mileage);
                _logger.LogInformation("S-a creat configurația '{ConfigName}' pentru vehiculul ID {VehicleId} cu intervalele: {Mileage} km / {Time} zile.", newConfig.Name, vehicle.Id, newConfig.MileageIntervalConfig, newConfig.DateIntervalConfig);
            }
        }
    }