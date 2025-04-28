namespace CarTrack_API.EntityLayer.Models;

public class Status
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Reminder> Reminders { get; set; } = new();
    
}