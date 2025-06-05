using CarTrack_API.EntityLayer.Dtos.BodyDto;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using CarTrack_API.EntityLayer.Dtos.VehicleEngineDto;
using CarTrack_API.EntityLayer.Dtos.VehicleInfo;
using CarTrack_API.EntityLayer.Dtos.VehicleModelDto;
using CarTrack_API.EntityLayer.Dtos.VehicleUsageStatsDto;

namespace CarTrack_API.BusinessLogic.Services.VehicleService;

public interface IVehicleService
{
    Task<List<VehicleResponseDto>> GetAllByClientIdAsync(int clientId);
    Task AddVehicleAsync( VehicleRequestDto request);
    Task <VehicleEngineResponseDto> GetVehicleEngineByVehicleIdAsync(int vehId);
    Task<VehicleModelResponseDto> GetVehicleModelByVehicleIdAsync(int vehId);
    Task<VehicleInfoResponseDto> GetVehicleInfoByVehicleIdAsync(int vehId);
    Task<List<VehicleUsageStatsResponseDto>> GetVehicleUsageStatsByVehicleIdAsync(int vehId);
    Task<BodyResponseDto> GetVehicleBodyByVehicleIdAsync(int vehId);
    Task AddVehicleMaintenanceAsync(VehicleMaintenanceRequestDto request);
}