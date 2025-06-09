using System.Security.Claims;
using CarTrack_API.BusinessLogic.Services.VinDecoderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Authorize(Roles = "client")]
[Route("api/vindecoder")]
[ApiController]
public class VinDecoderController : ControllerBase
{
    private readonly IVinDecoderService _vinDecoderService;

    public VinDecoderController(IVinDecoderService vinDecoderService)
    {
        _vinDecoderService = vinDecoderService;
    }
    
    [HttpGet("{vin}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DecodeVin([FromRoute] string vin)
    {
        if (string.IsNullOrWhiteSpace(vin) || vin.Length != 17) 
        {
            return BadRequest("Invalid VIN format.");
        }
            
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            
        var vinDecoded = await _vinDecoderService.DecodeVinAsync(vin, userId);
            
        return Ok(vinDecoded);
    }
}