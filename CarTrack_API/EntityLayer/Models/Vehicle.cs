namespace CarTrack_API.EntityLayer.Models;

public class Vehicle
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public ClientProfile Client { get; set; }
    public int VehicleModelId { get; set; }
    public VehicleModel VehicleModel { get; set; }
    public List<Appointment> Appointments = new();
    public List<VehiclePaper> VehiclePapers = new();
    public List<MaintenanceVerifiedRecord> MaintenanceVerifiedRecord;
    public List<MaintenanceUnverifiedRecord> MaintenanceUnverifiedRecord;
    public VehicleInfo VehicleInfo { get; set; }
    public List<VehicleUsageStats> VehicleUsageStats { get; set; }
    public List<VehicleMaintenanceConfig> VehicleMaintenanceConfigs { get; set; }
    public List<Reminder> Reminders { get; set; }

}