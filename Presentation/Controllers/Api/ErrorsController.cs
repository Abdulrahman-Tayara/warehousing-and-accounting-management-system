using Microsoft.AspNetCore.Mvc;

namespace wms.Controllers.Api;

public class ErrorsController : ControllerBase
{
    [HttpGet("Error400")]
    public IActionResult Error400()
    {
        return BadRequest();
    }
    
    [HttpGet("Error401")]
    public IActionResult Error401()
    {
        return Unauthorized();
    }
    
    [HttpGet("Error403")]
    public IActionResult Error403()
    {
        return StatusCode(403);
    }
    
    [HttpGet("Error404")]
    public IActionResult Error404()
    {
        return NotFound();
    }
    
    [HttpGet("Error500")]
    public IActionResult Error500()
    {
        return StatusCode(500);
    }
}