using Microsoft.AspNetCore.Mvc;
using wms.Dto.Responses.Common;

namespace wms.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    [NonAction]
    public override OkObjectResult Ok(object? value)
    {
        return Ok(value, "Success");
    }

    [NonAction]
    public OkObjectResult Ok(object? value, string message)
    {
        if (value == null)
        {
            return base.Ok(new NoDataResponse());
        }
        else
        {
            return base.Ok(
                new BaseResponse<object>(
                    new ResponseMetaData() {message = message},
                    data: value
                )
            );
        }
    }
}