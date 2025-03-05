using System.Data;

namespace CarTrack_API.Models;

public class UserRole
{
    public int Id { get; set; }
    public required string Role { get; set; }
    
    public List<User> Users { get; set; } = new();
    
}