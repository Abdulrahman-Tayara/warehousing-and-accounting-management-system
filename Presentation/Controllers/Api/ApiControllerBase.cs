using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common.Responses;

namespace wms.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    protected IMediator Mediator;
    protected IMapper Mapper;

    public ApiControllerBase(IMediator mediator, IMapper mapper)
    {
        Mediator = mediator;
        Mapper = mapper;
    }

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