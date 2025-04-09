using CarTrack_API.DataAccess.Dtos.AppointmentDto;
using CarTrack_API.DataAccess.Dtos.RepairShopDto;

namespace CarTrack_API.DataAccess.Dtos.MechanicProfileDto;

public class MechanicProfileResponseDto
{
    public RepairShopResponseDto RepairShop { get; set; }
    public List<AppointmentResponseDto> Appointments = new();
}