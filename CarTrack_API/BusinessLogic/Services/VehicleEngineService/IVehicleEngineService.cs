using CarTrack_API.EntityLayer.Dtos.VehicleEngineDto;

namespace CarTrack_API.BusinessLogic.Services.VehicleEngineService;

public interface IVehicleEngineService
{
    Task<VehicleEngineResponseDto> GetVehicleEngineByVehicleId(int vehId);
}