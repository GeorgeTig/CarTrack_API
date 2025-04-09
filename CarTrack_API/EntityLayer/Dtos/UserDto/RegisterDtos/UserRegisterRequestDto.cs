namespace CarTrack_API.EntityLayer.Dtos.UserDto.RegisterDtos;

public class UserRegisterRequestDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public int PhoneNumber { get; set; }
    public int RoleId { get; set; }
    
    
}