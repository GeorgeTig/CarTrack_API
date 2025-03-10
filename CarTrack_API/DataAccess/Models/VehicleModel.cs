namespace CarTrack_API.Models;

public class VehicleModel
{
    public int Id { get; set; }
    public required string Series { get; set; }
    public required string Brand { get; set; }
    public int Year { get; set; }
    public required string BodyType { get; set; }
    public int DoorNumber { get; set; }
    public required string TransmissionType { get; set; }
    public required string WheelDriveType { get; set; } //front wheel, back wheel, 
    public int FuelTankCapacity { get; set; }
    
    public int VehicleEngineId { get; set; }
    public required VehicleEngine VehicleEngine { get; set; }
    public List<Vehicle> Vehicles { get; set; } = new();
}