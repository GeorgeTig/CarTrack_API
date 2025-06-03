namespace CarTrack_API.EntityLayer.Models;

public class User
{
    public int  Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public int PhoneNumber { get; set; }
    public Boolean IsActive { get; set; }
    public int RoleId { get; set; }
    public UserRole Role { get; set; }
    public ClientProfile ClientProfile { get; set; } 
    public ManagerProfile ManagerProfile { get; set; }
    public MechanicProfile MechanicProfile { get; set; }
    public List<Notification> Notifications = new();
    public RefreshToken RefreshToken { get; set; }


}