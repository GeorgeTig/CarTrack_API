namespace CarTrack_API.DataAccess.Dtos.RegisterDtos;

public class UserRegisterRequestDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
}