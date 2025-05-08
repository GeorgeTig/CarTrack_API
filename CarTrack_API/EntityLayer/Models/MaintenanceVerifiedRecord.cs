namespace CarTrack_API.EntityLayer.Models;

public class MaintenanceVerifiedRecord
{
    public int Id { get; set; }
    public DateTime DoneDate { get; set; }
    public decimal Cost { get; set; }
    
    public int VehicleId { get; set; }
    public required Vehicle Vehicle { get; set; }
    
    // na verified maintennance can be done from an appointment or from the user input 
    public List<Appointment> Appointments { get; set; } = new(); 
    public List<string> MaintenanceNames { get; set; } = new();

}