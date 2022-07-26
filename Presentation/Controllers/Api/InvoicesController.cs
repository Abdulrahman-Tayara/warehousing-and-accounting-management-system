using Application.Commands.Invoicing;
using Application.Queries.Invoicing;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Invoices;
using wms.Dto.Pagination;
using StatusCodes = Domain.Exceptions.StatusCodes;

namespace wms.Controllers.Api;

[Authorize]
public class InvoicesController : ApiControllerBase
{
    public InvoicesController(IMediator mediator, IMapper mapper) :
        base(mediator, mapper)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ActionResult<BaseResponse<InvoiceViewModel>>), 200)]
    [ProducesResponseType(typeof(ActionResult<BaseResponse<IList<int>>>),
        StatusCodes.ProductMinLevelExceededExceptionCode)]
    public async Task<ActionResult<BaseResponse<InvoiceViewModel>>> Create(CreateInvoiceRequest request)
    {
        var createInvoiceCommand = Mapper.Map<CreateInvoiceCommand>(request);
        createInvoiceCommand.AccountType = InvoiceAccountType.PurchasesSales;

        var invoiceId = await Mediator.Send(createInvoiceCommand);

        var invoice = await Mediator.Send(new GetInvoiceQuery {Id = invoiceId});

        return Ok(invoice.ToViewModel<InvoiceViewModel>(Mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<InvoiceViewModel>>> Get(int id)
    {
        var invoice = await Mediator.Send(new GetInvoiceQuery {Id = id});

        return Ok(invoice.ToViewModel<InvoiceViewModel>(Mapper));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<InvoiceViewModel>>>> GetAll(
        [FromQuery] InvoicesQueryParams request)
    {
        var query = request.AsQuery<GetAllInvoicesQuery>(Mapper);

        var invoices = await Mediator.Send(query);

        return Ok(invoices.ToViewModel<InvoiceViewModel>(Mapper));
    }

    [HttpGet("{id}/items")]
    public async Task<ActionResult<BaseResponse<IEnumerable<ProductMovementViewModel>>>> GetInvoiceItems(int id)
    {
        var items = await Mediator.Send(new GetProductMovementsQuery {InvoiceId = id});

        return Ok(items.ToViewModel<ProductMovementViewModel>(Mapper));
    }
}