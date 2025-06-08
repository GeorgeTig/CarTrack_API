    using CarTrack_API.EntityLayer.Dtos.VehicleInfo;
    using CarTrack_API.EntityLayer.Models;

    namespace CarTrack_API.BusinessLogic.Mapping;

    public static class MappingVehicleInfo
    {
        public static VehicleInfoResponseDto ToVehicleInfoResponseDto(this VehicleInfo vehicleInfo)
        {
            var vehicleInfoResponseDto = new VehicleInfoResponseDto
            {
                Mileage = vehicleInfo.Mileage,
                AverageTravelDistance = vehicleInfo.AverageTravelDistance,
                LastUpdate = vehicleInfo.LastUpdate.ToString("o") 
            };

            return vehicleInfoResponseDto;
        }
    }