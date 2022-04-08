using Application.Commands.Currencies.CreateCurrency;
using Application.Queries.Currencies;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Currencies;

namespace wms.Controllers.Api;

[Authorize]
public class CurrenciesController : ApiControllerBase
{
    public CurrenciesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<CurrencyViewModel>>> CreateCurrency(CreateCurrencyRequest request)
    {
        var command = Mapper.Map<CreateCurrencyCommand>(request);

        var currencyId = await Mediator.Send(command);

        var currency = await Mediator.Send(new GetCurrencyQuery {Id = currencyId});

        return Ok(currency.ToViewModel<CurrencyViewModel>(Mapper));
    }
    
    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<CurrencyViewModel>>>> GetAll()
    {
        var currencies = await Mediator.Send(new GetAllCurrenciesQuery());

        return Ok(currencies.ToViewModels<CurrencyViewModel>(Mapper));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<CurrencyViewModel>>> GetCurrency(int id)
    {
        var currencies = await Mediator.Send(new GetCurrencyQuery{Id = id});

        return Ok(currencies.ToViewModel<CurrencyViewModel>(Mapper));
    }
}