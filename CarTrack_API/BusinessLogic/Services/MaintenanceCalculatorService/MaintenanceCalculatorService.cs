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
            string filePath = Path.Combine(AppContext.BaseDirectory, "maintenance_modifiers.json");
            string jsonString = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<MaintenanceRuleDto>>(jsonString, options) ??
                   new List<MaintenanceRuleDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading the maintenance rules file 'maintenance_modifiers.json'.");
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
                object vehicleValue = GetPropertyValue(vehicle, modifier.Property);
                if (vehicleValue != null)
                {
                    foreach (var caseDto in modifier.Cases)
                    {
                        if (CheckCondition(vehicleValue, caseDto.Operator, caseDto.Value))
                        {
                            if (finalMileage != -1) finalMileage *= caseDto.Factor;
                            if (finalTime != -1) finalTime *= caseDto.Factor;
                            break;
                        }
                    }
                }
            }
            
            int finalMileageInt = (finalMileage != -1) ? RoundToNearestThousand((int)finalMileage) : -1;
            int finalTimeInt =
                (int)Math.Round(
                    finalTime); 

            if (finalMileageInt == 0 || finalTimeInt == 0)
            {
                _logger.LogWarning(
                    "Maintenance plan for '{MaintenanceName}' resulted in a '0' interval after calculation or rounding. Skipping.",
                    rule.MaintenanceName);
                continue;
            }

            if (finalMileageInt < 0 && finalTimeInt < 0)
            {
                _logger.LogWarning("Invalid maintenance plan for '{MaintenanceName}'. Both intervals are inactive.",
                    rule.MaintenanceName);
                continue;
            }

            calculatedPlan.Add(new CalculatedMaintenancePlan
            {
                Name = rule.MaintenanceName,
                TypeId = rule.MaintenanceTypeId,
                MileageInterval = finalMileageInt,
                TimeInterval = finalTimeInt
            });
        }

        return calculatedPlan;
    }
    
    private int RoundToNearestThousand(int number)
    {
        if (number <= 0) return number; 
        if (number < 1000) return 1000; 

        int remainder = number % 1000;
        
        if (remainder >= 500)
        {
            return (number - remainder) + 1000;
        }
        else
        {
            return number - remainder;
        }
    }

    private bool CheckCondition(object vehicleValue, string op, object ruleValue)
    {
        if (ruleValue is JsonElement element)
        {
            if (vehicleValue is string)
            {
                ruleValue = element.GetString();
            }
            else if (vehicleValue is int or double or long)
            {
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
            var property = currentObject.GetType()
                .GetProperty(part, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null) return null;
            currentObject = property.GetValue(currentObject);
        }

        return currentObject;
    }
}