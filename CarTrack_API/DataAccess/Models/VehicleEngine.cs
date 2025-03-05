namespace CarTrack_API.Models;

public class VehicleEngine
{
    public int Id { get; set; }
    public required string EngineType { get; set; }
    public required string FuelType { get; set; }
    public required string Size { get; set; } // Engine size in Liters
    public int HorsePower { get; set; }
    public int Torque { get; set; }
    public int Cylinders { get; set; }
    public required string Name { get; set; }
    
    public List<VehicleModel> VehicleModels { get; set; } = new();
    
}