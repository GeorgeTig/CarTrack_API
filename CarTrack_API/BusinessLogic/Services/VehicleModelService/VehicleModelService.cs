using CarTrack_API.DataAccess.Repositories.VehicleModelRepository;
using CarTrack_API.EntityLayer.Dtos.VinDto.VinDeserializedDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.VehicleModelService;

public class VehicleModelService(IVehicleModelRepository vehicleModelRepository) : IVehicleModelService
{
    private readonly IVehicleModelRepository _vehicleModelRepository = vehicleModelRepository;
    
    
    public async Task<List<VehicleModel>> GetAllByVinDtoAsync(VehicleVinDeserializedDto vinDeserializedDto)
    {
        var vehicleModel = await _vehicleModelRepository.GetAllByVinDtoAsync(vinDeserializedDto);
        return vehicleModel;
    }
    
    
}