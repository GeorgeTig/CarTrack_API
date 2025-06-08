using CarTrack_API.BusinessLogic.Services.VinDecoderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Authorize(Roles = "client")]
[Route("api/vindecoder")]
public class VinDecoderController(IVinDecoderService vinDecoderService) : ControllerBase
{
    private readonly IVinDecoderService _vinDecoderService = vinDecoderService;
    
    [HttpGet("{vin}")] // Am eliminat clientId din rută
    public async Task<IActionResult> DecodeVin([FromRoute] string vin)
    {
        if (string.IsNullOrWhiteSpace(vin) || vin.Length != 17) 
        {
            return BadRequest("Invalid VIN format.");
        }
        
        var vinDecoded = await _vinDecoderService.DecodeVinAsync(vin);
        
        // Nu mai e nevoie să returnezi BadRequest dacă e gol, un array gol e un răspuns valid
        return Ok(vinDecoded);
    }
}