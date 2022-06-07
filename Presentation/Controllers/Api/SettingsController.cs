using Application.Settings;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common.Responses;

namespace wms.Controllers.Api;

[Authorize]
public class SettingsController : ApiControllerBase
{
    public SettingsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    public ActionResult<BaseResponse<ApplicationSettings>> Get([FromServices] ApplicationSettings settings)
    {
        return Ok(settings);
    }
}