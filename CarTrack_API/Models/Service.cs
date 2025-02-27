namespace CarTrack_API.Models;

public class Service
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string ContactEmail { get; set; }
    public int PhoneNumber { get; set; }
    public Boolean IsActive { get; set; }
    
    public int ManagerId { get; set; }
    public User Manager { get; set; }
    
    public List<Appointment> Appointments { get; set; } = new();
    public List<Mechanic_Service> MechanicService { get; set; } = new();
    public List<MaintenanceRecord> MaintenanceRecords { get; set; } = new();
    
}