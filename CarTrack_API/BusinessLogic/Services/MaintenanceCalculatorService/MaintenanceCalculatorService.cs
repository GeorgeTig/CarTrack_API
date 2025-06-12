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
            return JsonSerializer.Deserialize<List<MaintenanceRuleDto>>(jsonString, options) ?? new List<MaintenanceRuleDto>();
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
            
            // Doar dacă intervalul este activ (nu e -1), aplicăm modificatorii.
            // Acest lucru previne ca un factor de 0.8 să transforme -1 în -0.8, de exemplu.
            if (finalMileage != -1 && finalTime != -1)
            {
                 foreach (var modifier in rule.Modifiers)
                {
                    object vehicleValue = GetPropertyValue(vehicle, modifier.Property);

                    if (vehicleValue != null)
                    {
                        foreach (var caseDto in modifier.Cases)
                        {
                            if (CheckCondition(vehicleValue, caseDto.Operator, caseDto.Value))
                            {
                                // Aplicăm factorii doar pe valorile care nu sunt -1
                                if (finalMileage != -1) finalMileage *= caseDto.Factor;
                                if (finalTime != -1) finalTime *= caseDto.Factor;
                                break; 
                            }
                        }
                    }
                }
            }
            
            // Convertim rezultatele la int după toate calculele
            int finalMileageInt = (int)Math.Round(finalMileage);
            int finalTimeInt = (int)Math.Round(finalTime);

            // --- NOUA LOGICĂ DE VALIDARE ȘI ADĂUGARE ---

            // Validare 1: Asigură-te că nu avem intervale 0, care sunt ambigue.
            if (finalMileageInt == 0 || finalTimeInt == 0)
            {
                _logger.LogWarning(
                    "Maintenance plan for '{MaintenanceName}' resulted in a '0' interval. " +
                    "Please use -1 for inactive intervals in 'maintenance_modifiers.json'. Skipping this item.",
                    rule.MaintenanceName);
                continue; // Sarim peste acest element de plan, deoarece este configurat greșit
            }
            
            // Validare 2: Asigură-te că cel puțin un interval este activ.
            if (finalMileageInt < 0 && finalTimeInt < 0)
            {
                _logger.LogWarning(
                    "Invalid maintenance plan generated for '{MaintenanceName}'. " +
                    "Both mileage and time intervals are inactive (-1). Skipping this item.",
                    rule.MaintenanceName);
                continue; // Sarim peste acest element, deoarece nu are niciun criteriu de scadență
            }

            // Dacă a trecut de validări, adăugăm planul.
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

    // Metodele ajutătoare CheckCondition și GetPropertyValue rămân neschimbate
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
            var property = currentObject.GetType().GetProperty(part, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null) return null;
            currentObject = property.GetValue(currentObject);
        }
        return currentObject;
    }
}