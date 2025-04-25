using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.BusinessLogic.Services.VehicleMaintenanceConfigService;
using CarTrack_API.DataAccess.Repositories.VehicleRepository;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using CarTrack_API.EntityLayer.Exceptions.VehicleException;
using Newtonsoft.Json;

namespace CarTrack_API.BusinessLogic.Services.VehicleService;

public class VehicleService(IVehicleRepository vehicleRepository, HttpClient httpClient, IVehicleMaintenanceConfigService vehicleConfigService) : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
    private readonly HttpClient _httpClient = httpClient;
    private readonly IVehicleMaintenanceConfigService _vehicleConfigService = vehicleConfigService;
    
    public async Task<List<VehicleResponseDto>> GetAllByClientIdAsync(int id)
    {
        var vehicles =await _vehicleRepository.GetAllByClientIdAsync(id);
        var vehicleResponseDtos = vehicles.ToListVehicleResponseDto();
        return vehicleResponseDtos;
    }

    public async Task AddVehicleAsync( VehicleRequestDto vehicleRequestDto)
    {
        try
        {
            var vehicle = vehicleRequestDto.ToVehicle();
            await _vehicleRepository.AddVehicleAsync(vehicle);
            await _vehicleConfigService.DefaultMaintenanceConfigAsync(vehicle.Id);
        }
        catch (VehicleAlreadyExistException )
        {
            throw;
        }
        
    }

}