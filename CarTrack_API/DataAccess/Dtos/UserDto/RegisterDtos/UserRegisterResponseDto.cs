using CarTrack_API.DataAccess.Dtos.ClientProfileDto;
using CarTrack_API.DataAccess.Dtos.ManagerProfileDto;
using CarTrack_API.DataAccess.Dtos.MechanicProfileDto;

namespace CarTrack_API.DataAccess.Dtos.RegisterDtos;

public class UserRegisterResponseDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
    public string PhoneNumber { get; set; }
    
    public ClientProfileResponseDto ClientProfile { get; set; }
    public ManagerProfileResponseDto ManagerProfile { get; set; }
    public MechanicProfileResponseDto MechanicProfile { get; set; }
}