namespace CarTrack_API.Models;

public class User
{
    public int  Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public int PhoneNumber { get; set; }
    public int VehicleSlots { get; set; }
    public Boolean IsActive { get; set; }
    
    public int RoleId { get; set; }
    public UserRole Role { get; set; }
    
    public List<Vehicle> Vehicles { get; set; } = new();
    public List<VehiclePapers> VehiclePapers { get; set; } = new();
    public List<Notification> Notifications { get; set; } = new();
    public List<Service> Services { get; set; } = new();
    public List<Appointment> Appointments { get; set; } = new();
    public List<Mechanic_Service> MechanicService { get; set; } = new();
    public List<MaintenanceRecord> MaintenanceRecords { get; set; } = new();
}