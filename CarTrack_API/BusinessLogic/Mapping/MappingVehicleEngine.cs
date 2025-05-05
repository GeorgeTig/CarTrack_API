using CarTrack_API.EntityLayer.Dtos.VehicleEngineDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingVehicleEngine
{
    public static VehicleEngineResponseDto ToVehicleEngineResponseDto(this VehicleEngine request)
    {
        var vehicleEngine = new VehicleEngineResponseDto
        {
            Id = request.Id,
            EngineType = request.EngineType,
            FuelType = request.FuelType,
            Cylinders = request.Cylinders,
            Size = request.Size,
            HorsePower = request.Horsepower,
            TorqueFtLbs = request.TorqueFtLbs,
            DriveType = request.DriveType,
            Transmission = request.Transmission,
        };
        return vehicleEngine;
    }
}