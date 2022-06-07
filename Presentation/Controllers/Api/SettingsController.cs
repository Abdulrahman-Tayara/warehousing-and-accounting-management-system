using Application.Settings;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common.Responses;

namespace wms.Controllers.Api;

public class SettingsController : ApiControllerBase
{
    private readonly ApplicationSettings settings;

    public SettingsController(IMediator mediator, IMapper mapper, ApplicationSettings settings) : base(mediator, mapper)
    {
        this.settings = settings;
    }


    [HttpGet]
    public ActionResult<BaseResponse<ApplicationSettings>> Get()
    {
        return Ok(settings);
    }
}