namespace CarTrack_API.EntityLayer.Models;

public class VehicleMaintenanceConfig
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int DateIntervalConfig { get; set; } // in days
    public int MileageIntervalConfig { get; set; } // in km
    public bool IsEditable { get; set; }
    
    public MaintenanceType MaintenanceType { get; set; }
    public int MaintenanceTypeId { get; set; }
    
    public Reminder Reminder { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
}