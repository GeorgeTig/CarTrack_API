using CarTrack_API.BusinessLogic.Services.VehicleModelService;
using CarTrack_API.BusinessLogic.Services.VehicleService;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Authorize(Roles = "client")]
[Route ("api/vehicle")]
public class VehicleController( IVehicleService vehicleService, IVehicleModelService vehicleModelService) : ControllerBase
{
    private readonly IVehicleService _vehicleService = vehicleService;
    private readonly IVehicleModelService _vehicleModelService = vehicleModelService;

    [HttpGet ("{clientId}")]
    public ActionResult<List<VehicleResponseDto>> GetAll([FromRoute] int clientId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var vehicles = _vehicleService.GetAllByClientIdAsync(clientId);
        return Ok(vehicles);
    }
    
    
    
}