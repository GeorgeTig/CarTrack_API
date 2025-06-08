using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.MaintenanceCalculatorService;

public class CalculatedMaintenancePlan
{
    public string Name { get; set; }
    public int TypeId { get; set; }
    public int MileageInterval { get; set; }
    public int TimeInterval { get; set; }
}
    
public interface IMaintenanceCalculatorService
{
    List<CalculatedMaintenancePlan> GeneratePlanForVehicle(Vehicle vehicle);
}