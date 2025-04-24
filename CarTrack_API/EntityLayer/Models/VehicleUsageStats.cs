namespace CarTrack_API.EntityLayer.Models;

public class VehicleUsageStats
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Distance { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    
}