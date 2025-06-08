namespace CarTrack_API.EntityLayer.Dtos.UserDto;

public class ChangePasswordRequestDto
{
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}