namespace CarTrack_API.Models;

public class VehicleModel
{
    public int Id { get; set; }
    public int Year { get; set; }
    public required string SeriesName { get; set; }
    public required string ModelFullName { get; set; }
    public long FuelTankCapacity { get; set; } // in gallon
    public long Consumption { get; set; } // The consumption is in mile/gallon
    
    
    public int BodyId { get; set; }
    public required Body Body { get; set; }
    public int ProducerId { get; set; }
    public required Producer Producer { get; set; }
    public int VehicleEngineId { get; set; }
    public required VehicleEngine VehicleEngine { get; set; }
    public List<Vehicle> Vehicles { get; set; } = new();
}