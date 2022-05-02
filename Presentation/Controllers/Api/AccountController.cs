using Application.Commands.Accounts;
using Application.Queries.Accounts;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Accounts;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Pagination;

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

        var id = await Mediator.Send(command);

        return await GetAccount(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<AccountViewModel>>> GetAccount(int id)
    {
        var query = new GetAccountQuery {Id = id};

        var unitEntity = await Mediator.Send(query);

        return Ok(unitEntity.ToViewModel<AccountViewModel>(Mapper));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<PageViewModel<AccountViewModel>>>> GetAccounts(
        [FromQuery] PaginationRequestParams request
    )
    {
        var query = request.AsQuery<GetAllAccountsQuery>();

        var accountsPage = await Mediator.Send(query);

        return Ok(accountsPage.ToViewModel<AccountViewModel>(Mapper));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<AccountViewModel>>> UpdateAccount(UpdateAccountRequest request,
        int id)
    {
        var command = Mapper.Map<UpdateAccountCommand>(request);
        command.Id = id;

        var updatedAccountId = await Mediator.Send(command);

        return await GetAccount(updatedAccountId);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAccount(int id)
    {
        await Mediator.Send(new DeleteAccountCommand {key = id});
    }
}