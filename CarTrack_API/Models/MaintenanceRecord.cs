namespace CarTrack_API.Models;

public class MaintenanceRecord
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public required string Description { get; set; }
    public double Cost { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int MechanicId { get; set; }
    public User Mechanic { get; set; }
    
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    
}