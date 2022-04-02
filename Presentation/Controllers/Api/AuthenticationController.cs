using Authentication.Dto;
using Authentication.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Authentication;
using wms.Dto.Common.Responses;

namespace wms.Controllers.Api;

public class AuthenticationController : ApiControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IMediator mediator, IMapper mapper,
        IAuthenticationService authenticationService) : base(mediator)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ActionResult<BaseResponse<string>>> Login(LoginRequest request)
    {
        var result = await  _authenticationService.JwtLogin(_mapper.Map<JwtLoginRequest>(request));

        return Ok(new LoginResponse()
        {
            User = result.User,
            Token = result.Token
        });
    }
}