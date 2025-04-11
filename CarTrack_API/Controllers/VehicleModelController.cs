using CarTrack_API.BusinessLogic.Services.VehicleModelService;
using CarTrack_API.BusinessLogic.Services.VinDecoderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Authorize(Roles = "client")]
[Route("api/vehiclemodel")]
public class VehicleModelController(IVehicleModelService vehicleModelService, IVinDecoderService vinDecoderService): ControllerBase
{
    private readonly IVehicleModelService _vehicleModelService = vehicleModelService;
    private readonly IVinDecoderService _vinDecoderService = vinDecoderService;
    
    [HttpGet ("decodevin/{vin}/{clientId}")]
    public async Task<IActionResult> DecodeVin([FromRoute] string vin, [FromRoute] int clientId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vinDecoded = await _vinDecoderService.DecodeVinAsync(vin);
        
        if (vinDecoded == null)
        {
            return BadRequest(ModelState);
        }
        
        return Ok(vinDecoded);
    }
}