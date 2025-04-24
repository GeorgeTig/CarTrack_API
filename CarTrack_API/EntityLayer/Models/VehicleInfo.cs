namespace CarTrack_API.EntityLayer.Models;

public class VehicleInfo
{
    public Vehicle Vehicle { get; set; }
    public int VehicleId { get; set; }
    public double Mileage { get; set; }
    public double TravelDistanceAVG { get; set; }
    public DateTime LastUpdate { get; set; }
}