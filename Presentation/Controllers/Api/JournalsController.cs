using Application.Queries.Accounting;
using Application.Settings;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Accounting;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Journals;
using wms.Dto.Pagination;

namespace wms.Controllers.Api;

[Authorize]
public class JournalsController : ApiControllerBase
{
    public JournalsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<JournalViewModel>>> GetJournals(
        [FromQuery] GetAllJournalsQueryParams request)
    {
        var query = request.AsQuery<GetJournalEntriesQuery>(Mapper);

        var journals = await Mediator.Send(query);

        var viewmodel = journals.ToViewModel<JournalViewModel>(Mapper);

        return Ok(viewmodel);
    }

    [HttpGet("cashDrawer")]
    public async Task<ActionResult<BaseResponse<JournalViewModel>>> GetJournals(
        [FromQuery] GetAllCashDrawerJournalsQueryParams request, [FromServices] ApplicationSettings settings)
    {
        var query = request.AsQuery<GetJournalEntriesQuery>(Mapper);
        query.FromAccountId = settings.DefaultMainCashDrawerAccountId;

        var journals = await Mediator.Send(query);

        var viewmodel = journals.ToViewModel<JournalViewModel>(Mapper);

        return Ok(viewmodel);
    }
}