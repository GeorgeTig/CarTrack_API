using CarTrack_API.BusinessLogic.Services.VehicleModelService;
using CarTrack_API.BusinessLogic.Services.VehicleService;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTrack_API.Controllers;

[Authorize(Roles = "client")]
[Route("api/vehicle")]
public class VehicleController(IVehicleService vehicleService, IVehicleModelService vehicleModelService)
    : ControllerBase
{
    private readonly IVehicleService _vehicleService = vehicleService;
    private readonly IVehicleModelService _vehicleModelService = vehicleModelService;

    [HttpGet("{clientId}")]
    public ActionResult<List<VehicleResponseDto>> GetAll([FromRoute] int clientId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicles = _vehicleService.GetAllByClientIdAsync(clientId);
        return Ok(vehicles);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddVehicle([FromBody] VehicleRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _vehicleService.AddVehicleAsync(request);
        return Ok();
    }
    
    [HttpGet("engine/{vehId}")]
    public async Task<IActionResult> GetVehicleEngineByVehicleId([FromRoute] int vehId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicleEngine = await _vehicleService.GetVehicleEngineByVehicleIdAsync(vehId);
        return Ok(vehicleEngine);
    }
    
    [HttpGet("model/{vehId}")]
    public async Task<IActionResult> GetVehicleModelByVehicleId([FromRoute] int vehId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicleModel = await _vehicleService.GetVehicleModelByVehicleIdAsync( vehId);
        return Ok(vehicleModel);
    }
    
    [HttpGet("info/{vehId}")]
    public async Task<IActionResult> GetVehicleInfoByVehicleId([FromRoute] int vehId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicleInfo = await _vehicleService.GetVehicleInfoByVehicleIdAsync(vehId);
        return Ok(vehicleInfo);
    }
    
    [HttpGet("usage/{vehId}")]
    public async Task<IActionResult> GetVehicleUsageStatsByVehicleId([FromRoute] int vehId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicleUsageStats = await _vehicleService.GetVehicleUsageStatsByVehicleIdAsync(vehId);
        return Ok(vehicleUsageStats);
    }
    
    [HttpGet("body/{vehId}")]
    public async Task<IActionResult> GetVehicleBodyByVehicleId([FromRoute] int vehId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicleBody = await _vehicleService.GetVehicleBodyByVehicleIdAsync(vehId);
        return Ok(vehicleBody);
    }
}