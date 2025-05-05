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
                Series = item.VehicleModel.SeriesName,
                Year = item.VehicleModel.Year
            };
            vehicleResponseDtos.Add(vehicleResponseDto);
        }
        

        return vehicleResponseDtos;
    }

    public static Vehicle ToVehicle(this VehicleRequestDto request)
    {
        var vehicle = new Vehicle
        {
            Vin = request.Vin,
            VehicleModelId = request.ModelId,
            ClientId = request.ClientId,
            VehicleInfo = new VehicleInfo
            {
                Mileage = request.Mileage,
                TravelDistanceAVG = 0,
                LastUpdate = DateTime.UtcNow
            }
        };
        return vehicle;
    }
  
}