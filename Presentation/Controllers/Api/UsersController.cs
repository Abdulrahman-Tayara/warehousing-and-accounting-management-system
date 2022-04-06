using Application.Commands.Users.CreateUser;
using Application.Commands.Users.DeleteUser;
using Application.Commands.Users.UpdateUser;
using Application.Queries.Users;
using AutoMapper;
using Infrastructure.Models;
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

        return Ok(user.ToViewModel<UserViewModel>(Mapper));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<BaseResponse<IEnumerable<UserViewModel>>>> GetUsers()
    {
        var result = await Mediator.Send(new GetAllUsersQuery());

        return Ok(result.ToViewModels<UserViewModel>(Mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<UserViewModel>>> GetUser(int id)
    {
        var user = await Mediator.Send(new GetUserQuery {Id = id});

        return Ok(user.ToViewModel<UserViewModel>(Mapper));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<object?>>> DeleteUser(int id)
    {
        await Mediator.Send(new DeleteUserCommand {Id = id});

        return Ok(null);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<UserViewModel>>> UpdateUser(int id, UpdateUserRequest request)
    {
        var command = Mapper.Map<UpdateUserCommand>(request);
        command.Id = id;

        await Mediator.Send(command);

        var user =  await Mediator.Send(new GetUserQuery {Id = id});

        return Ok(user.ToViewModel<UserViewModel>(Mapper));
    }
}