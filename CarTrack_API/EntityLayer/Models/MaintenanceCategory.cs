namespace CarTrack_API.EntityLayer.Models;

public class MaintenanceCategory
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<VehicleMaintenanceConfig> VehicleMaintenanceConfigs { get; set; } = new ();
}