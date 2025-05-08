namespace CarTrack_API.EntityLayer.Models;

public class Deal
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Price { get; set; }
    public required string Description { get; set; }
    
    public int RepairShopId { get; set; }
    public RepairShop RepairShop { get; set; }
    public List<Appointment> Appointments { get; set; } = new();
}