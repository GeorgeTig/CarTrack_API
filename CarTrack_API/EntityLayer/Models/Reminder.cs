using CarTrack_API.Models;

namespace CarTrack_API.EntityLayer.Models;

public class Reminder
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public double? DueMileage { get; set; }
    public DateTime? DueDate { get; set; }
    
    public string Status { get; set; }
    
    public bool IsRead { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    
}