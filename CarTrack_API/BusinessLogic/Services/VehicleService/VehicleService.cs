using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.DataAccess.Repositories.VehicleRepository;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using Newtonsoft.Json;

namespace CarTrack_API.BusinessLogic.Services.VehicleService;

public class VehicleService(IVehicleRepository vehicleRepository, HttpClient httpClient) : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
    private readonly HttpClient _httpClient = httpClient;
    
    public async Task<List<VehicleResponseDto>> GetAllByClientIdAsync(int id)
    {
        var vehicles =await _vehicleRepository.GetAllByClientIdAsync(id);
        var vehicleResponseDtos = vehicles.ToListVehicleResponseDto();
        return vehicleResponseDtos;
    }
    
   
}