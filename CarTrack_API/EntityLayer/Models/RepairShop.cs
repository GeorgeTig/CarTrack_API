namespace CarTrack_API.EntityLayer.Models;

public class RepairShop
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Address { get; set; }
    public int PhoneNumber { get; set; }
    public Boolean IsActive { get; set; }

    public int ManagerId { get; set; }
    public required ManagerProfile Manager;
    public List<Deal> Deals { get; set; } = new();
    public List<Appointment> Appointments { get; set; } = new();
    public List<MechanicProfile> Mechanics { get; set; } = new();
    public List<MaintenanceVerifiedRecord> MaintenanceRecords { get; set; } = new();
    
}