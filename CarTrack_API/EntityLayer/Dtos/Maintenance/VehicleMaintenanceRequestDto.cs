using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.EntityLayer.Dtos.Maintenance;

public class VehicleMaintenanceRequestDto
{
    public int VehicleId { get; set; }
    public DateTime Date { get; set; }
    public double Mileage { get; set; }
    public List<MaintenanceItemInputDto> MaintenanceItems { get; set; } = new();
    public string ServiceProvider { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public decimal Cost { get; set; } = 0; 
}
