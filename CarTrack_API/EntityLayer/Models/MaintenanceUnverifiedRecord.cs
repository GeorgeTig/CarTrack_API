namespace CarTrack_API.EntityLayer.Models;

public class MaintenanceUnverifiedRecord
{
    public int Id { get; set; }
    public DateTime DoneDate { get; set; }
    public decimal Cost { get; set; }
    
    public int VehicleId { get; set; }
    public required Vehicle Vehicle { get; set; }
    
    // An unverified maintenance can be inputed by an user 
    public List<string> MaintenanceNames { get; set; } = new();
    
}