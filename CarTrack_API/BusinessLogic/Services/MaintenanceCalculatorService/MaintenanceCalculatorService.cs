using System.Reflection;
using System.Text.Json;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.MaintenanceCalculatorService;

    public class MaintenanceCalculatorService : IMaintenanceCalculatorService
    {
        private readonly List<MaintenanceRuleDto> _rules;
        private readonly ILogger<MaintenanceCalculatorService> _logger;

        public MaintenanceCalculatorService(ILogger<MaintenanceCalculatorService> logger)
        {
            _logger = logger;
            _rules = LoadMaintenanceRules();
        }

        private List<MaintenanceRuleDto> LoadMaintenanceRules()
        {
            try
            {
                // Presupunem că fișierul este în rădăcina proiectului și are "Copy to Output Directory" setat pe "Copy if newer"
                string filePath = Path.Combine(AppContext.BaseDirectory, "maintenance_modifiers.json");
                string jsonString = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<MaintenanceRuleDto>>(jsonString, options) ?? new List<MaintenanceRuleDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la încărcarea fișierului de reguli de mentenanță 'maintenance_modifiers.json'.");
                return new List<MaintenanceRuleDto>();
            }
        }

        public List<CalculatedMaintenancePlan> GeneratePlanForVehicle(Vehicle vehicle)
        {
            var calculatedPlan = new List<CalculatedMaintenancePlan>();

            foreach (var rule in _rules)
            {
                double finalMileage = rule.BaseInterval.Mileage;
                double finalTime = rule.BaseInterval.Time;

                foreach (var modifier in rule.Modifiers)
                {
                    // Obținem valoarea proprietății din obiectul 'vehicle' (ex: vehicle.VehicleModel.VehicleEngine.FuelType)
                    object vehicleValue = GetPropertyValue(vehicle, modifier.Property);

                    if (vehicleValue != null)
                    {
                        foreach (var caseDto in modifier.Cases)
                        {
                            if (CheckCondition(vehicleValue, caseDto.Operator, caseDto.Value))
                            {
                                finalMileage *= caseDto.Factor;
                                finalTime *= caseDto.Factor;
                                break; // Am găsit un caz care se potrivește, trecem la următorul modificator
                            }
                        }
                    }
                }

                if (finalMileage > 0 || finalTime > 0)
                {
                    calculatedPlan.Add(new CalculatedMaintenancePlan
                    {
                        Name = rule.MaintenanceName,
                        TypeId = rule.MaintenanceTypeId,
                        MileageInterval = (int)Math.Round(finalMileage),
                        TimeInterval = (int)Math.Round(finalTime)
                    });
                }
            }
            return calculatedPlan;
        }

        private bool CheckCondition(object vehicleValue, string op, object ruleValue)
        {
            // Convertim valoarea din JSON (care e un JsonElement) la tipul corect
            if (ruleValue is JsonElement element)
            {
                if (vehicleValue is string)
                {
                    ruleValue = element.GetString();
                }
                else if (vehicleValue is int or double or long)
                {
                    // Încercăm să convertim la double pentru comparații numerice
                    if (element.TryGetDouble(out double doubleVal))
                    {
                        ruleValue = doubleVal;
                    }
                }
            }
            
            return op.ToLower() switch
            {
                "contains" => vehicleValue.ToString().ToLower().Contains(ruleValue.ToString().ToLower()),
                "equals" => vehicleValue.ToString().ToLower().Equals(ruleValue.ToString().ToLower()),
                "greaterthan" => Convert.ToDouble(vehicleValue) > Convert.ToDouble(ruleValue),
                "lessthan" => Convert.ToDouble(vehicleValue) < Convert.ToDouble(ruleValue),
                _ => false
            };
        }

        private object GetPropertyValue(object obj, string propertyPath)
        {
            if (obj == null || string.IsNullOrEmpty(propertyPath)) return null;

            var currentObject = obj;
            var pathParts = propertyPath.Split('.');

            foreach (var part in pathParts)
            {
                if (currentObject == null) return null;
                var property = currentObject.GetType().GetProperty(part, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property == null) return null;
                currentObject = property.GetValue(currentObject);
            }
            return currentObject;
        }
    }

