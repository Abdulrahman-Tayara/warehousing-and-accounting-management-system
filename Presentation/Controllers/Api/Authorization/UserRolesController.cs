using Application.Commands.Authorization.UserRoles;
using Application.Common.Models;
using Application.Queries.Authorization.UserRoles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Authorization.Roles;
using wms.Dto.Authorization.UserRoles;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Pagination;

namespace wms.Controllers.Api.Authorization;

[Route("api/authorization/[controller]")]
public class UserRolesController : ApiControllerBase
{
    public UserRolesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<IPaginatedEnumerable<UserRolesViewModel>>>> GetUsersRoles()
    {
        var usersRoles = await Mediator.Send(new GetAllUsersRolesQuery());

        return Ok(usersRoles.ToViewModel<UserRolesViewModel>(Mapper));
    }
    
    [HttpGet("{userId}")]
    public async Task<ActionResult<BaseResponse<PageViewModel<RoleViewModel>>>> GetUserRoles(int userId)
    {
        var roles = await Mediator.Send(new GetUserRolesQuery {UserId = userId});

        return Ok(roles.ToViewModel<RoleViewModel>(Mapper));
    }

    [HttpPut("{userId}")]
    public async Task<ActionResult<BaseResponse<PageViewModel<RoleViewModel>>>> UpdateUserRoles(int userId,
        UpdateUserRolesRequest request)
    {
        var command = new UpdateUserRolesCommand {UserId = userId, RoleIds = request.RoleIds};

        await Mediator.Send(command);

        return await GetUserRoles(userId);
    }
}