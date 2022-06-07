using Application.Exceptions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace wms.Controllers.Api;

public class ErrorsController : ControllerBase
{
    [HttpGet("Error400")]
    public IActionResult Error400()
    {
        throw new BaseException("Some validation exception", 400);
    }
    
    [HttpGet("Error401")]
    public IActionResult Error401()
    {
        throw new BaseException("Unauthorized", 401);
    }
    
    [HttpGet("Error403")]
    public IActionResult Error403()
    {
        throw new BaseException("Forbidden", 401);
    }
    
    [HttpGet("Error404")]
    public IActionResult Error404()
    {
        throw new NotFoundException("Something", 1);
    }
    
    [HttpGet("Error500")]
    public IActionResult Error500()
    {
        throw new BaseException();
    }
}