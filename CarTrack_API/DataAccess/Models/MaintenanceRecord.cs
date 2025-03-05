namespace CarTrack_API.Models;

public class MaintenanceRecord
{
    public int Id { get; set; }
    public DateTime LastMaintenanceDate { get; set; }
    public decimal TotalCost { get; set; }
    
    public int VehicleId { get; set; }
    public required Vehicle Vehicle { get; set; }
    public List<Appointment> Appointments { get; set; } = new();

}