namespace CarTrack_API.EntityLayer.Models;

public class VehicleInfo
{
    public Vehicle Vehicle { get; set; }
    public int VehicleId { get; set; }
    
    public required string Vin { get; set; }
    public double Mileage { get; set; }
}