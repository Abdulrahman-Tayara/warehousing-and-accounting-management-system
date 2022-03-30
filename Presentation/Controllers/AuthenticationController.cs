using MediatR;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Requests;
using wms.Dto.Responses.Common;
using wms.Dto.ViewModels;

namespace wms.Controllers;

public class AuthenticationController : ApiControllerBase
{
    public AuthenticationController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("authenticate")]
    public ActionResult<BaseResponse<AuthenticatedUserViewModel>> Authenticate(AuthenticateRequestBody body)
    {
        var authenticatedUser = _Authenticate(body);

        return Ok(authenticatedUser, "Authentication success");
    }

    private AuthenticatedUserViewModel _Authenticate(AuthenticateRequestBody body)
    {
        return new AuthenticatedUserViewModel()
        {
            Token = "eyfiowjeiouqwhfgnwipejcnxowuehfiuqiwubqndijwniwqu",
            UserViewModel = new UserViewModel()
            {
                Username = body.UserName
            }
        };
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(new {name = "Alaa Abo Muner", proffession = "Amazing"});
    }
}