using CarTrack_API.EntityLayer.Dtos.VinDto.VinDecodedDto;
using CarTrack_API.EntityLayer.Dtos.VinDto.VinDeserializedDto;

namespace CarTrack_API.BusinessLogic.Services.VinDecoderService;

public interface IVinDecoderService
{
    Task<List<VinDecodedResponnseDto>> DecodeVinAsync(string vin);
}