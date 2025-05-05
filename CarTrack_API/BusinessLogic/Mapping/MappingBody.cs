using CarTrack_API.EntityLayer.Dtos.BodyDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingBody
{
    public static BodyResponseDto ToBodyResponseDto(this Body vehicleBody)
    {
        var vehicleBodyResponseDto = new BodyResponseDto
        {
            Id = vehicleBody.Id,
            BodyType = vehicleBody.BodyType,
            DoorNumber = vehicleBody.DoorNumber,
            SeatNumber = vehicleBody.SeatNumber
        };

        return vehicleBodyResponseDto;
    }
}