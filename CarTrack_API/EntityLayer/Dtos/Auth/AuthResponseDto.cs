namespace CarTrack_API.EntityLayer.Dtos.Auth;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}