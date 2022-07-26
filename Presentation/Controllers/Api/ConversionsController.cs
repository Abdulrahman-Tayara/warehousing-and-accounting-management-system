using Application.Commands.Conversions;
using Application.Queries.Conversions;
using Application.Queries.Invoicing;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Conversions;
using wms.Dto.Invoices;
using wms.Dto.Pagination;

namespace wms.Controllers.Api;

[Authorize]
public class ConversionsController : ApiControllerBase
{
    public ConversionsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<PageViewModel<ConversionViewModel>>>> GetAll()
    {
        var getAllQuery = new GetAllConversionsQuery();

        var conversionsPage = await Mediator.Send(getAllQuery);

        return Ok(conversionsPage.ToViewModel<ConversionViewModel>(Mapper));
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BaseResponse<ConversionViewModel>>> Get(int id)
    {
        var conversionsPage = await Mediator.Send(new GetAllConversionsQuery());

        var conversion = conversionsPage.First(conversion => conversion.Id == id);

        return Ok(conversion.ToViewModel<ConversionViewModel>(Mapper));
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<ConversionViewModel>>> Create(CreateConversionRequest request)
    {
        var createInvoiceCommand = Mapper.Map<CreateConversionCommand>(request);
        var conversionId = await Mediator.Send(createInvoiceCommand);

        var conversionsPage = await Mediator.Send(new GetAllConversionsQuery());
        var conversion = conversionsPage.First(conversion => conversion.Id == conversionId);
        return Ok(conversion.ToViewModel<ConversionViewModel>(Mapper));
    }
}