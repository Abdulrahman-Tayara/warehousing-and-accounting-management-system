using Application.Common.Security;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Authorization.Policies;
using wms.Dto.Common.Responses;

namespace wms.Controllers.Api.Authorization;

[Route("api/authorization/[controller]")]
public class PoliciesController : ApiControllerBase
{
    public PoliciesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    public ActionResult<BaseResponse<GetPoliciesResponse>> GetAll()
    {
        return Ok(
            new GetPoliciesResponse()
            {
                Methods = Enum.GetValues<Method>(),
                Resources = Enum.GetValues<Resource>(),
            });
    }
}