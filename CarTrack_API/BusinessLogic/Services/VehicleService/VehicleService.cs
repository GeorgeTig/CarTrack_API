using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.BusinessLogic.Services.ReminderService;
using CarTrack_API.BusinessLogic.Services.VehicleMaintenanceConfigService;
using CarTrack_API.DataAccess.Repositories.VehicleRepository;
using CarTrack_API.EntityLayer.Dtos.BodyDto;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using CarTrack_API.EntityLayer.Dtos.VehicleEngineDto;
using CarTrack_API.EntityLayer.Dtos.VehicleInfo;
using CarTrack_API.EntityLayer.Dtos.VehicleModelDto;
using CarTrack_API.EntityLayer.Dtos.VehicleUsageStatsDto;
using CarTrack_API.EntityLayer.Exceptions.VehicleException;

namespace CarTrack_API.BusinessLogic.Services.VehicleService;

public class VehicleService(IVehicleRepository vehicleRepository, IVehicleMaintenanceConfigService vehicleConfigService) : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
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
    
    public async Task<VehicleEngineResponseDto> GetVehicleEngineByVehicleIdAsync(int vehId)
    {
        var vehicle = await _vehicleRepository.GetVehicleEngineByVehicleIdAsync(vehId);
        if (vehicle == null)
        {
            throw new VehicleNotFoundException("VehicleEngine not found");
        }
        
        var vehicleEngineResponseDto = vehicle.ToVehicleEngineResponseDto();
        
        return vehicleEngineResponseDto ;
    }
    
    public async Task<VehicleModelResponseDto> GetVehicleModelByVehicleIdAsync(int vehId)
    {
        var vehicle = await _vehicleRepository.GetVehicleModelByVehicleIdAsync(vehId);
        if (vehicle == null)
        {
            throw new VehicleNotFoundException("VehicleModel not found");
        }
        
        var vehicleModelResponseDto = vehicle.ToVehicleModelResponseDto();
        
        return vehicleModelResponseDto ;
    }
    
    public async Task<VehicleInfoResponseDto> GetVehicleInfoByVehicleIdAsync(int vehId)
    {
        var vehicle = await _vehicleRepository.GetVehicleInfoByVehicleIdAsync(vehId);
        if (vehicle == null)
        {
            throw new VehicleNotFoundException("VehicleInfo not found");
        }
        
        var vehicleInfoResponseDto = vehicle.ToVehicleInfoResponseDto();
        
        return vehicleInfoResponseDto ;
    }
    
    public async Task<BodyResponseDto> GetVehicleBodyByVehicleIdAsync(int vehId)
    {
        var vehicle = await _vehicleRepository.GetVehicleBodyByVehicleIdAsync(vehId);
        if (vehicle == null)
        {
            throw new VehicleNotFoundException("Vehicle not found");
        }

        var vehicleBody = vehicle.ToBodyResponseDto();
        
        return vehicleBody;
    }

    public async Task AddVehicleMaintenanceAsync(VehicleMaintenanceRequestDto vehRequest)
    {
        var maintenance = MappingMaintenanceRecord.ToMaintenanceUnverifiedRecord(vehRequest);
        await _vehicleRepository.AddVehicleMaintenanceAsync(maintenance);
    }
    
    public async Task<List<MaintenanceLogDto>> GetMaintenanceHistoryAsync(int vehId)
    {
        var maintenanceLog = await _vehicleRepository.GetVehicleMaintenanceByVehicleIdAsync(vehId);
        if (maintenanceLog == null)
        {
            throw new VehicleNotFoundException("Vehicle maintenance log not found");
        }

        var maintenanceLogDto = maintenanceLog.ToMaintenanceLogDtoList();
        
        return maintenanceLogDto;
    }
}