using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingVehicle
{
    public static List<VehicleResponseDto> ToListVehicleResponseDto(this List<Vehicle> vehicle)
    {
        var vehicleResponseDtos = new List<VehicleResponseDto>();
        foreach (var item in vehicle)
        {
            var vehicleResponseDto = new VehicleResponseDto
            {
                Id = item.Id,
                Vin = item.Vin,
                Mileage = item.Mileage,
                ModelName = item.VehicleModel.SeriesName,
                Year = item.VehicleModel.Year
            };
            vehicleResponseDtos.Add(vehicleResponseDto);
        }
        

        return vehicleResponseDtos;
    }
    
  
}