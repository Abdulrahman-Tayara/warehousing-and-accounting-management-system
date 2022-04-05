using Application.Commands.Users.CreateUser;
using Application.Queries.Users;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Users;

namespace wms.Controllers.Api;

public class UsersController : ApiControllerBase
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger, IMapper mapper,
        IMediator mediator) : base(mediator, mapper)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<UserViewModel>>> CreateUser(CreateUserRequest request)
    {
        var command = Mapper.Map<CreateUserCommand>(request);

        var userId = await Mediator.Send(command);

        var user = await Mediator.Send(new GetUserQuery()
        {
            Id = userId
        });

        return Ok(user.ToViewModel(Mapper));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<BaseResponse<IEnumerable<UserViewModel>>>> GetUsers()
    {
        var result = await Mediator.Send(new GetAllUsersQuery());

        return Ok(result.ToViewModels(Mapper));
    }
}