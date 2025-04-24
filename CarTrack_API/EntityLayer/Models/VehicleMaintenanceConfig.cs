namespace CarTrack_API.EntityLayer.Models;

public class VehicleMaintenanceConfig
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public DateTime? DateIntervalConfig { get; set; }
    public double? MileageIntervalConfig { get; set; }
    
    public bool IsEditable { get; set; }
    
    public MaintenanceType MaintenanceType { get; set; }
    public int MaintenanceTypeId { get; set; }
    
    public MaintenanceCategory MaintenanceCategory { get; set; }
    public int MaintenanceCategoryId { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
}