using Application.Commands.CountryOrigins;
using Application.Queries.CountryOrigins;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.CountryOrigins;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Pagination;

namespace wms.Controllers.Api;

[Authorize]
public class CountryOriginsController : ApiControllerBase
{
    public CountryOriginsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<CountryOriginViewModel>>> CreateCountryOrigin(CreateCountryOriginRequest request)
    {
        var command = Mapper.Map<CreateCountryOriginCommand>(request);

        var countryOriginId = await Mediator.Send(command);

        return await GetCountryOrigin(countryOriginId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<CountryOriginViewModel>>> GetCountryOrigin(int id)
    {
        var query = new GetCountryOriginQuery {Id = id};

        var countryOriginEntity = await Mediator.Send(query);

        return Ok(countryOriginEntity.ToViewModel<CountryOriginViewModel>(Mapper));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<PageViewModel<CountryOriginViewModel>>>> CountryOrigins(
        [FromQuery] CountryOriginsQueryParams request)
    {
        var query = request.AsQuery<GetAllCountryOriginsQuery>(Mapper);

        var countryOriginEntities = await Mediator.Send(query);

        return Ok(countryOriginEntities.ToViewModel<CountryOriginViewModel>(Mapper));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<CountryOriginViewModel>>> UpdateCountryOrigin(UpdateCountryOriginRequest request,
        int id)
    {
        var command = Mapper.Map<UpdateCountryOriginCommand>(request);
        command.Id = id;

        var updatedCountryOriginId = await Mediator.Send(command);

        return await GetCountryOrigin(updatedCountryOriginId);
    }
    
    [HttpDelete("{id}")]
    public async Task DeleteCountryOrigin(int id)
    {
        await Mediator.Send(new DeleteCountryOriginCommand() {key = id});
    }
}