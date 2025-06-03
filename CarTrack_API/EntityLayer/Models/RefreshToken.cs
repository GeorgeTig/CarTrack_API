namespace CarTrack_API.EntityLayer.Models;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRevoked { get; set; } = false;

    // FK
    public int UserId { get; set; }
    public User User { get; set; }
}