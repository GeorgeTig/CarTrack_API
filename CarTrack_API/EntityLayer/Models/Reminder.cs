namespace CarTrack_API.EntityLayer.Models;

public class Reminder
{
    public int VehicleMaintenanceConfigId { get; set; }
    public VehicleMaintenanceConfig VehicleMaintenanceConfig { get; set; }
  
    public double LastMileageCkeck { get; set; }
    public DateTime LastDateCheck { get; set; }
    
    public int StatusId { get; set; }
    public Status Status { get; set; }
    
    public bool IsActive { get; set; }
}