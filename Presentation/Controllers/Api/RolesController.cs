using Application.Commands.Roles;
using Application.Queries.Roles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Pagination;
using wms.Dto.Roles;

namespace wms.Controllers.Api;

public class RolesController : ApiControllerBase
{
    public RolesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<RoleViewModel>>> Create(CreateRoleRequest request)
    {
        var command = Mapper.Map<CreateRoleRequest, CreateRoleCommand>(request);
        
        var roleId = await Mediator.Send(command);

        var role = await Mediator.Send(new GetRoleQuery {Id = roleId});
        
        return Ok(role.ToViewModel<RoleViewModel>(Mapper));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<PageViewModel<RoleViewModel>>>> GetRoles()
    {
        var roles = await Mediator.Send(new GetAllRolesQuery());

        return Ok(roles.ToViewModel<RoleViewModel>(Mapper));
    }

    [HttpDelete("{id}")]
    public Task Delete(int id)
    {
        return Mediator.Send(new DeleteRoleCommand {Id = id});
    }
}