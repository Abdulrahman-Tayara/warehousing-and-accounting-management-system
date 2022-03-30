using Application.Commands.Users.CreateUser;
using Application.Queries.Users;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Requests.Users;

namespace wms.Controllers;

public class UsersController : ApiControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IMapper _mapper;

    public UsersController(ILogger<UsersController> logger, IMapper mapper,
        IMediator mediator) : base(mediator)
    {
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(CreateUserRequest request)
    {
        var command = _mapper.Map<CreateUserCommand>(request);

        var userId = await Mediator.Send(command);

        var user = await Mediator.Send(new GetUserQuery()
        {
            Id = userId
        });

        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var result = await Mediator.Send(new GetAllUsersQuery());

        return Ok(result);
    }
}