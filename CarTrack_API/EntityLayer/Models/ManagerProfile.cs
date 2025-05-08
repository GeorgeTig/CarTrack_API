namespace CarTrack_API.EntityLayer.Models;

public class ManagerProfile
{
    public int UserId { get; set; }
    public User User { get; set; }
    public List<RepairShop> RepairShops { get; set; } = new();
}