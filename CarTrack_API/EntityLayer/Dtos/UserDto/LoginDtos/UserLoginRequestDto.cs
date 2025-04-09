namespace CarTrack_API.EntityLayer.Dtos.UserDto.LoginDtos;

// public class UserLoginRequestDto
// {
//     public string Email { get; set; }
//     public string Password { get; set; }
// }

public record UserLoginRequestDto(string Email, string Password);