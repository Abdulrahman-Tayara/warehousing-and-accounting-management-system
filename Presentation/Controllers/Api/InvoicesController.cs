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
        var invoiceId = await Mediator.Send(Mapper.Map<CreateInvoiceCommand>(request));

        var invoice = await Mediator.Send(new GetInvoiceQuery {Id = invoiceId});

        return Ok(invoice.ToViewModel<InvoiceViewModel>(Mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<InvoiceViewModel>>> Get(int id)
    {
        var invoice = await Mediator.Send(new GetInvoiceQuery {Id = id});

        return Ok(invoice.ToViewModel<InvoiceViewModel>(Mapper));
    }

    [HttpGet("purchases")]
    public async Task<ActionResult<BaseResponse<IEnumerable<InvoiceViewModel>>>> GetAllPurchasesInvoices(
        [FromQuery] PaginationRequestParams request)
    {
        var query = request.AsQuery(new GetAllInvoicesQuery
        {
            Type = InvoiceType.In
        });

        var invoices = await Mediator.Send(query);

        return Ok(invoices.ToViewModel<InvoiceViewModel>(Mapper));
    }
    
    [HttpGet("sales")]
    public async Task<ActionResult<BaseResponse<IEnumerable<InvoiceViewModel>>>> GetAllSalesInvoices(
        [FromQuery] PaginationRequestParams request)
    {
        var query = request.AsQuery(new GetAllInvoicesQuery
        {
            Type = InvoiceType.Out
        });

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