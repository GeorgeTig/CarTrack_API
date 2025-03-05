using System.Runtime.InteropServices.JavaScript;

namespace CarTrack_API.Models;

public class Vehicle
{
    public int Id { get; set; }
    public int Vin { get; set; }
    public int Mileage { get; set; }
    
    public int ClientId { get; set; }
    public required ClientProfile Client { get; set; }
    public int VehicleModelId { get; set; }
    public required VehicleModel VehicleModel { get; set; }
    public List<Appointment> Appointments = new();
    public List<VehiclePaper> VehiclePapers = new();
    public MaintenanceRecord MaintenanceRecord;

}