namespace CarTrack_API.EntityLayer.Dtos.UserDto;

public class InitiateChangeEmailRequestDto
{
    public required string CurrentPassword { get; set; }
    public required string NewEmail { get; set; }
}