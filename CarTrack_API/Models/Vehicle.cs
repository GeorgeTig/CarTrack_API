using System.Runtime.InteropServices.JavaScript;

namespace CarTrack_API.Models;

public class Vehicle
{
    public int Id { get; set; }
    public int Vin { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required string Year { get; set; }
    public int Mileage { get; set; }
    public required string VehicleBodyType { get; set; }
    public Boolean IsActive { get; set; }
    
    public int EngineId { get; set; }
    public Engine Engine { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public List<VehiclePapers> VehiclePapers { get; set; } = new();
    public List<MaintenanceRecord> MaintenanceRecords { get; set; } = new();
    
}