using CarTrack_API.DataAccess.Dtos.ClientProfileDto;
using CarTrack_API.DataAccess.Dtos.ManagerProfileDto;
using CarTrack_API.DataAccess.Dtos.MechanicProfileDto;
using CarTrack_API.Models;

namespace CarTrack_API.DataAccess.Dtos.LoginDtos;

public class UserLoginResponseDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Token { get; set; }
    
    public UserRole Role { get; set; }
    public ClientProfileResponseDto ClientProfile { get; set; }
    public ManagerProfileResponseDto ManagerProfile { get; set; }
    public MechanicProfileResponseDto MechanicProfile { get; set; }
}