using CarTrack_API.EntityLayer.Dtos.BodyDto;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.Usage;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using CarTrack_API.EntityLayer.Dtos.VehicleEngineDto;
using CarTrack_API.EntityLayer.Dtos.VehicleInfo;
using CarTrack_API.EntityLayer.Dtos.VehicleModelDto;
using CarTrack_API.EntityLayer.Dtos.VehicleUsageStatsDto;

namespace CarTrack_API.BusinessLogic.Services.VehicleService;

public interface IVehicleService
{
    Task<List<VehicleResponseDto>> GetAllByClientIdAsync(int clientId);
    Task AddVehicleAsync(VehicleRequestDto request);
    Task AddVehicleMaintenanceAsync(VehicleMaintenanceRequestDto request);
    Task AddMileageReadingAsync(int vehicleId, AddMileageReadingRequestDto request);
    
    Task<VehicleEngineResponseDto> GetVehicleEngineByVehicleIdAsync(int vehicleId);
    Task<VehicleModelResponseDto> GetVehicleModelByVehicleIdAsync(int vehicleId);
    Task<VehicleInfoResponseDto> GetVehicleInfoByVehicleIdAsync(int vehicleId);
    Task<BodyResponseDto> GetVehicleBodyByVehicleIdAsync(int vehicleId);
    Task<bool> UserOwnsVehicleAsync(int userId, int vehicleId);
    Task DeactivateVehicleAsync(int vehicleId);


    
    Task<List<MaintenanceLogDto>> GetMaintenanceHistoryAsync(int vehicleId);
    Task<List<DailyUsageDto>> GetDailyUsageForLastWeekAsync(int vehicleId, string timeZoneId);
}