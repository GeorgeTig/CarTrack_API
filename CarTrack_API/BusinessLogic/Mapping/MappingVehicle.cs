using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingVehicle
{
    public static List<VehicleResponseDto> ToListVehicleResponseDto(this List<Vehicle> vehicles)
    {
        var vehicleResponseDtos = new List<VehicleResponseDto>();
    
        // Verificăm dacă lista primită nu este null
        if (vehicles == null)
        {
            return vehicleResponseDtos;
        }

        foreach (var vehicle in vehicles)
        {
            // Verificăm dacă vehiculul în sine nu este null
            if (vehicle != null)
            {
                // Cream un DTO cu valori default (goale)
                var vehicleResponseDto = new VehicleResponseDto
                {
                    Id = vehicle.Id,
                    Vin = "N/A", // Valoare default
                    Series = "Unknown Series", // Valoare default
                    Year = 0 // Valoare default
                };

                // Populăm câmpurile doar dacă obiectele relaționate nu sunt null
                if (vehicle.VehicleInfo != null)
                {
                    vehicleResponseDto.Vin = vehicle.VehicleInfo.Vin;
                }

                if (vehicle.VehicleModel != null)
                {
                    vehicleResponseDto.Series = vehicle.VehicleModel.SeriesName;
                    vehicleResponseDto.Year = vehicle.VehicleModel.Year;
                }
            
                vehicleResponseDtos.Add(vehicleResponseDto);
            }
        }
    
        return vehicleResponseDtos;
    }

    public static Vehicle ToVehicle(this VehicleRequestDto request)
    {
        var vehicle = new Vehicle
        {
            VehicleModelId = request.ModelId,
            ClientId = request.ClientId,
            VehicleInfo = new VehicleInfo
            {
                Mileage = request.Mileage,
                Vin = request.Vin,
                AverageTravelDistance = 60 // default 60 km/day
            }
        };
        return vehicle;
    }
  
}