namespace CarTrack_API.DataAccess.Dtos.LoginDtos;

public class UserLoginResponseDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string RoleName { get; set; }
}