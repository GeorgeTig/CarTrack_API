namespace CarTrack_API.EntityLayer.Models;

public class Notification
{
    public int Id { get; set; }
    public required string Message { get; set; }
    public DateTime Date { get; set; }
    
    public Boolean IsRead { get; set; }
    public bool IsActive { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    
    public int RemiderId { get; set; }
    public Reminder Reminder { get; set; }
}