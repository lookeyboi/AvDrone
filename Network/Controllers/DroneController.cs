using Microsoft.AspNetCore.Mvc;
using Network.Models;
using Network.Models.Response;

namespace Network.Controllers;

[ApiController]
[Route("[controller]")]
public class DronesController : ControllerBase
{
    private readonly IDroneService _droneService; // interface type

    public DronesController(IDroneService droneService) // ask for the interface
    {
        _droneService = droneService;
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
           DroneResponse drone = await _droneService.GetDroneByIdAsync(id);
           return Ok(drone);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
