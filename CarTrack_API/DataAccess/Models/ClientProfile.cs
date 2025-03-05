namespace CarTrack_API.Models;

public class ClientProfile
{
    public int UserId { get; set; }
    public required User User { get; set; }
    public List<Vehicle> Vehicles { get; set; } = new();
}