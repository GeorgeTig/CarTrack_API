using CarTrack_API.EntityLayer.Dtos.VinDto.VinDeserializedDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.VehicleModelRepository;

public interface IVehicleModelRepository
{
   Task<List<VehicleModel>> GetAllByVinDtoAsync(VehicleVinDeserializedDto vinDeserializedDto);
}