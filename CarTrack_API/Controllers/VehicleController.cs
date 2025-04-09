using CarTrack_API.BusinessLogic.Services.VehicleService;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Authorize(Roles = "client")]
[Route ("api/vehicle")]
public class VehicleController( IVehicleService vehicleService) : ControllerBase
{
    private readonly IVehicleService _vehicleService = vehicleService;

    [HttpGet ("{clientId}")]
    public ActionResult<List<VehicleResponseDto>> GetAll([FromRoute] int clientId)
    {
        var vehicles = _vehicleService.GetAllVehiclesByClientIdAsync(clientId);
        return Ok(vehicles);
    }
    
    
}