namespace CarTrack_API.Models;

public class Notification
{
    public int Id { get; set; }
    public required string Message { get; set; }
    public DateTime Date { get; set; }
    public Boolean IsRead { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}