using CarTrack_API.EntityLayer.Dtos.VinDto.VinDecodedDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingVehicleModel
{
   public static List<VinDecodedResponnseDto> ToVinDecodedResponnseDto(List<VehicleModel> vehicleModels)
   {
      var groupedBySeries = vehicleModels
         .GroupBy(vm => new { vm.SeriesName, Producer = vm.Producer.Name });

      var vinDecodedDtos = new List<VinDecodedResponnseDto>();

      foreach (var group in groupedBySeries)
      {
         var vinDto = new VinDecodedResponnseDto
         {
            SeriesName = group.Key.SeriesName,
            Producer = group.Key.Producer,
            VehicleModelInfo = group.Select(vm => new ModelDecodedDto
            {
               Year = vm.Year,
               ModelId = vm.Id,
               EngineInfo = new List<EngineDecodedDto>
               {
                  new EngineDecodedDto
                  {
                     EngineId = vm.VehicleEngine.Id,
                     EngineType = vm.VehicleEngine.EngineType,
                     DriveType = vm.VehicleEngine.DriveType,
                     Size = vm.VehicleEngine.Size,
                     Horsepower = vm.VehicleEngine.Horsepower,
                     Transmission = vm.VehicleEngine.Transmission
                  }
               },
               BodyInfo = new List<BodyDecodedDto>
               {
                  new BodyDecodedDto
                  {
                     BodyId = vm.Body.Id,
                     BodyType = vm.Body.BodyType,
                     DoorNumber = vm.Body.DoorNumber,
                     SeatNumber = vm.Body.SeatNumber
                  }
               }
            }).ToList()
         };

         vinDecodedDtos.Add(vinDto);
      }

      return vinDecodedDtos;
   }
}