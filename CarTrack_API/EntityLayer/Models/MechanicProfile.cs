namespace CarTrack_API.Models;

public class MechanicProfile
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int RepairShopId { get; set; }
    public RepairShop RepairShop { get; set; }
    public List<Appointment> Appointments { get; set; } = new();
}