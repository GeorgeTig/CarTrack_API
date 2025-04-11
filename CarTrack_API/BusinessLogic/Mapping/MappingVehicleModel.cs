using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using CarTrack_API.EntityLayer.Dtos.VehicleModelDto;
using CarTrack_API.EntityLayer.Dtos.VinDto.VinDecodedDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingVehicleModel
{
   public static List<VinDecodedResponnseDto> ToVinDecodedResponnseDto(List<VehicleModel> vehicleModels)
   {
      var vinDecodedResponnseDtos = new List<VinDecodedResponnseDto>();
      foreach (var vm in vehicleModels)
      {
         var decodedResponnseDto = new VinDecodedResponnseDto
         {
            ModelId = vm.Id,
            SeriesName = vm.SeriesName,
            Year = vm.Year,
            DriveType = vm.VehicleEngine.DriveType,
            Cylinders = vm.VehicleEngine.Cylinders,
            Size = vm.VehicleEngine.Size,
            Horsepower = vm.VehicleEngine.Horsepower,
            TorqueFtLbs = vm.VehicleEngine.TorqueFtLbs,
            Doornumber = vm.Body.DoorNumber,
            Seatnumber = vm.Body.SeatNumber
         };
         vinDecodedResponnseDtos.Add(decodedResponnseDto);
      }

      return vinDecodedResponnseDtos;
   }
}