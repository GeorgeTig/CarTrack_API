namespace CarTrack_API.Models;

public class Producer
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public List<VehicleModel> VehicleModels { get; set; } = new();
}