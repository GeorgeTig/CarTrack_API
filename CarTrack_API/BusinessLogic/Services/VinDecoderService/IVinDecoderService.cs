using CarTrack_API.EntityLayer.Dtos.VinDto.VinDecodedDto;

namespace CarTrack_API.BusinessLogic.Services.VinDecoderService;

public interface IVinDecoderService
{
    Task<List<VinDecodedResponnseDto>> DecodeVinAsync(string vin);
}