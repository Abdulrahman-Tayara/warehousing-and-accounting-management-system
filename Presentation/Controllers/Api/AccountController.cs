using Application.Commands.Accounts;
using Application.Queries.Accounts;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Accounts;
using wms.Dto.Common;
using wms.Dto.Common.Responses;

namespace wms.Controllers.Api;

[Authorize]
public class AccountController : ApiControllerBase
{
    public AccountController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<AccountViewModel>>> CreateAccount(CreateAccountRequest request)
    {
        var command = Mapper.Map<CreateAccountCommand>(request);

        var unitId = await Mediator.Send(command);

        return await GetAccount(unitId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<AccountViewModel>>> GetAccount(int id)
    {
        var query = new GetAccountQuery {Id = id};

        var unitEntity = await Mediator.Send(query);

        return Ok(unitEntity.ToViewModel<AccountViewModel>(Mapper));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<AccountViewModel>>>> GetAccounts()
    {
        var query = new GetAllAccountsQuery();

        var unitEntities = await Mediator.Send(query);

        return Ok(unitEntities.ToViewModels<AccountViewModel>(Mapper));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<AccountViewModel>>> CreateAccount(UpdateAccountRequest request,
        int id)
    {
        var command = Mapper.Map<UpdateAccountCommand>(request);
        command.Id = id;

        var updatedAccountId = await Mediator.Send(command);

        return await GetAccount(updatedAccountId);
    }
}