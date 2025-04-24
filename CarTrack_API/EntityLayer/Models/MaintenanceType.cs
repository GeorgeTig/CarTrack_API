namespace CarTrack_API.EntityLayer.Models;

public class MaintenanceType
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<VehicleMaintenanceConfig> VehicleMaintenanceConfigs { get; set; } = new ();
}