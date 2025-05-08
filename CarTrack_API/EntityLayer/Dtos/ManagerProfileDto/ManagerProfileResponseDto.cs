using CarTrack_API.DataAccess.Dtos.AppointmentDto;
using CarTrack_API.DataAccess.Dtos.RepairShopDto;

namespace CarTrack_API.DataAccess.Dtos.ManagerProfileDto;

public class ManagerProfileResponseDto
{
    private List<RepairShopResponseDto> RepairShops = new();
    private List<AppointmentResponseDto> Managers = new();
}