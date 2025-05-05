using CarTrack_API.EntityLayer.Dtos.VehicleUsageStatsDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingVehicleUsageStats
{
    public static List<VehicleUsageStatsResponseDto> ToListVehicleUsageResponseDto(this List<VehicleUsageStats> vehicleUsage)
    {
        var list = new List<VehicleUsageStatsResponseDto>();
        
        foreach (var item in vehicleUsage)
        {
            var vehicleUsageResponseDto = new VehicleUsageStatsResponseDto
            {
                Id = item.Id,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Distance = item.Distance
            };
            list.Add(vehicleUsageResponseDto);
        }


        return list;
    }
}