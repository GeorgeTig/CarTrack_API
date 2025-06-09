using CarTrack_API.EntityLayer.Dtos.VinDto.VinDecodedDto;

namespace CarTrack_API.BusinessLogic.Services.VinDecoderService;

public interface IVinDecoderService
{
    Task<List<VinDecodedResponseDto>> DecodeVinAsync(string vin, int userId);
    
}