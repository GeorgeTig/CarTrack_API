using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.DataAccess.Repositories.VehicleRepository;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;

namespace CarTrack_API.BusinessLogic.Services.VehicleService;

public class VehicleService(IVehicleRepository vehicleRepository) : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
    
    public async Task<List<VehicleResponseDto>> GetAllVehiclesByClientIdAsync(int id)
    {
        var vehicles =await _vehicleRepository.GetAllByClientIdAsync(id);
        var vehicleResponseDtos = vehicles.ToListVehicleResponseDto();
        return vehicleResponseDtos;
    }
}