using CarTrack_API.DataAccess.Repositories.VehicleEngineRepository;
using CarTrack_API.EntityLayer.Dtos.VehicleEngineDto;

namespace CarTrack_API.BusinessLogic.Services.VehicleEngineService;

public class VehicleEngineService(IVehicleEngineRepository engineRepository) : IVehicleEngineService
{
    private readonly IVehicleEngineRepository _engineRepository = engineRepository;
    public async Task<VehicleEngineResponseDto> GetVehicleEngineByVehicleId(int vehId)
    {
        

        return null;
    }
}