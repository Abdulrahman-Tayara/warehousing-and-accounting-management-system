using System.Net;
using Application.Commands.Invoicing;
using Application.Queries.Invoicing;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Invoices;
using StatusCodes = Domain.Exceptions.StatusCodes;

namespace wms.Controllers.Api;

public class InvoiceController : ApiControllerBase
{
    public InvoiceController(IMediator mediator, IMapper mapper) :
        base(mediator, mapper)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(ActionResult<BaseResponse<InvoiceViewModel>>), 200)]
    [ProducesResponseType(typeof(ActionResult<BaseResponse<IList<int>>>), StatusCodes.ProductMinLevelExceededExceptionCode)]
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
}