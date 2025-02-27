namespace CarTrack_API.Models;

public class Engine
{
    public int Id { get; set; }
    public required string EngineFuelType { get; set; }
    public int HorsePower { get; set; }
    public int Torque { get; set; }
    public int Cylinders { get; set; }
    public required string Name { get; set; }
    
    public List<Vehicle> Vehicles { get; set; } = new();
}