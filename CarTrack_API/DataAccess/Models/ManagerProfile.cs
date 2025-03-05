namespace CarTrack_API.Models;

public class ManagerProfile
{
    public int UserId { get; set; }
    public required User User { get; set; }
    public List<RepairShop> RepairShops { get; set; } = new();
}