using CarTrack_API.DataAccess.Dtos.ClientProfileDto;
using CarTrack_API.DataAccess.Dtos.ManagerProfileDto;
using CarTrack_API.DataAccess.Dtos.MechanicProfileDto;
using CarTrack_API.Models;

namespace CarTrack_API.DataAccess.Dtos.RegisterDtos;

public class UserRegisterRequestDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PhoneNumber { get; set; }
    public UserRole Role { get; set; }
    
    
}