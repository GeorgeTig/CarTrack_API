namespace CarTrack_API.Models;

public class VehicleEngine
{
    public int Id { get; set; }
    public required string EngineType { get; set; }
    public required string FuelType { get; set; }
    public required string Cylinders { get; set; }
    public double Size { get; set; } // Engine size in Liters
    public int Horsepower { get; set; }
    public int TorqueFtLbs { get; set; }
    public required string DriveType { get; set; } // FWD, RWD, AWD, 4WD
    public required string Transmission { get; set; } // Automatic, Manual, CVT, DCT, DSG
    
    public List<VehicleModel> VehicleModels { get; set; } = new();
    
}