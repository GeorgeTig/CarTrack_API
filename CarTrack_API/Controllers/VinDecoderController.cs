using CarTrack_API.BusinessLogic.Services.VinDecoderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Authorize(Roles = "client")]
[Route("api/vindecoder")]
public class VinDecoderController(IVinDecoderService vinDecoderService): ControllerBase
{
    private readonly IVinDecoderService _vinDecoderService = vinDecoderService;
    
    [HttpGet ("{vin}/{clientId}")]
    public async Task<IActionResult> DecodeVin([FromRoute] string vin, [FromRoute] int clientId)
    {
        if (string.IsNullOrWhiteSpace(vin) || vin.Length != 17) 
        {
            return BadRequest("Invalid VIN format.");
        }

        if (clientId <= 0) 
        {
            return BadRequest("Invalid client ID.");
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vinDecoded = await _vinDecoderService.DecodeVinAsync(vin);
        
        if (vinDecoded.Count == 0)
        {
            return BadRequest(ModelState);
        }
        
        return Ok(vinDecoded);
    }
}