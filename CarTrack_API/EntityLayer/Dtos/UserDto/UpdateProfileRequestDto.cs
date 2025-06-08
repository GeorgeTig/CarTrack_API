namespace CarTrack_API.EntityLayer.Dtos.UserDto;

public class UpdateProfileRequestDto
{
    public required string Username { get; set; }
    public string? PhoneNumber { get; set; } 
}