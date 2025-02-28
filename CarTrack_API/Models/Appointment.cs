namespace CarTrack_API.Models;

public class Appointment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Boolean IsFinished { get; set; }
    public Boolean IsCancelled { get; set; }
    public required string Description { get; set; }
    public decimal Cost { get; set; }
    
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int MechanicId { get; set; }
    public User Mechanic { get; set; }
    
}