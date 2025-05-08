namespace CarTrack_API.EntityLayer.Models;

public class UserRole
{
    public int Id { get; set; }
    public required string Role { get; set; }
    
    public List<User> Users { get; set; } = new();
    
}