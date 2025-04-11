using CarTrack_API.EntityLayer.Dtos.VinDto.VinDeserializedDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.VehicleModelService;

public interface IVehicleModelService
{
    Task<List<VehicleModel>> GetAllByVinDtoAsync(VehicleVinDeserializedDto vinDeserializedDto);
}