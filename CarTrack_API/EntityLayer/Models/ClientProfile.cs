namespace CarTrack_API.EntityLayer.Models;

public class ClientProfile
{
    public int UserId { get; set; }
    public User User { get; set; }
    public List<Vehicle> Vehicles { get; set; } = new();
}