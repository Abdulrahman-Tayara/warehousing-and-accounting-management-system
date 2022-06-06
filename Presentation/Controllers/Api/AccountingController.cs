using Application.Queries.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Accounting;
using wms.Dto.Common;
using wms.Dto.Common.Responses;

namespace wms.Controllers.Api;

[Authorize]
public class AccountingController : ApiControllerBase
{
    public AccountingController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet("accountStatement")]
    public async Task<ActionResult<BaseResponse<AccountStatementViewModel>>> AccountStatement([FromQuery] int accountId)
    {
        var accountStatement = await Mediator.Send(new GetAccountStatementQuery(accountId));

        var viewmodel = accountStatement.ToViewModel<AccountStatementViewModel>(Mapper);

        return Ok(viewmodel);
    }
}