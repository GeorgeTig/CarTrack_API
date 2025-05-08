namespace CarTrack_API.EntityLayer.Models;

public class Appointment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Boolean IsFinished { get; set; }
    public Boolean IsCancelled { get; set; }
    public required string Description { get; set; }
    public decimal Cost { get; set; }

    public List<Deal> Deals { get; set; } = new();
    
    public int RepairShopId { get; set; }
    public required RepairShop RepairShop { get; set; }
    
    public int VehicleId { get; set; }
    public required Vehicle Vehicle { get; set; }
    
    public int MechanicId { get; set; }
    public MechanicProfile Mechanic { get; set; }
    public int MaintenanceRecordId { get; set; }
    public MaintenanceVerifiedRecord MaintenanceVerifiedRecord { get; set; }
    
}