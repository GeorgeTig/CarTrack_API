namespace CarTrack_API.EntityLayer.Models;

public class Body
{
    public int Id { get; set; }
    public required string BodyType { get; set; }
    public int DoorNumber { get; set; }
    public int SeatNumber { get; set; }
    
    public List<VehicleModel> VehicleModels { get; set; } = new();
}