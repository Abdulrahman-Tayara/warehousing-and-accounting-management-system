using Application.Commands.Units;
using Application.Queries.Units;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Units;
using wms.Dto.Common;
using wms.Dto.Common.Responses;

namespace wms.Controllers.Api;

[Authorize]
public class UnitsController : ApiControllerBase
{
    public UnitsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<UnitViewModel>>> CreateUnit(CreateUnitRequest request)
    {
        var command = Mapper.Map<CreateUnitCommand>(request);

        var unitId = await Mediator.Send(command);

        return await GetUnit(unitId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<UnitViewModel>>> GetUnit(int id)
    {
        var query = new GetUnitQuery {Id = id};

        var unitEntity = await Mediator.Send(query);

        return Ok(unitEntity.ToViewModel<UnitViewModel>(Mapper));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<UnitViewModel>>>> GetUnits()
    {
        var query = new GetAllUnitsQuery();

        var unitEntities = await Mediator.Send(query);

        return Ok(unitEntities.ToViewModels<UnitViewModel>(Mapper));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<UnitViewModel>>> CreateUnit(UpdateUnitRequest request,
        int id)
    {
        var command = Mapper.Map<UpdateUnitCommand>(request);
        command.Id = id;

        var updatedUnitId = await Mediator.Send(command);

        return await GetUnit(updatedUnitId);
    }
}