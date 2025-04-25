using CarTrack_API.EntityLayer.Dtos.VehicleDto;

namespace CarTrack_API.BusinessLogic.Services.VehicleService;

public interface IVehicleService
{
    Task<List<VehicleResponseDto>> GetAllByClientIdAsync(int clientId);
    Task AddVehicleAsync( VehicleRequestDto request);
}