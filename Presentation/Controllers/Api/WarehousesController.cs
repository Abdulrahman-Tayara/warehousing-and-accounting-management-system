using Application.Commands.Warehouses;
using Application.Queries.Warehouses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Pagination;
using wms.Dto.Warehouses;

namespace wms.Controllers.Api;

[Authorize]
public class WarehousesController : ApiControllerBase
{
    public WarehousesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<WarehouseViewModel>>> Create(CreateWarehouseRequest request)
    {
        var warehouseId = await Mediator.Send(Mapper.Map<CreateWarehouseCommand>(request));

        var warehouse = await Mediator.Send(new GetWarehouseQuery {Id = warehouseId});

        return Ok(warehouse.ToViewModel<WarehouseViewModel>(Mapper));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<WarehouseViewModel>>> Update(int id, UpdateWarehouseRequest request)
    {
        var command = Mapper.Map<UpdateWarehouseCommand>(request);
        command.Id = id;

        var warehouseId = await Mediator.Send(command);

        var warehouse = await Mediator.Send(new GetWarehouseQuery {Id = warehouseId});

        return Ok(warehouse.ToViewModel<WarehouseViewModel>(Mapper));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<PageViewModel<WarehouseViewModel>>>> GetAll(
        [FromQuery] PaginationRequestParams request)
    {
        var warehouses = await Mediator.Send(request.AsQuery<GetAllWarehousesQuery>());

        return Ok(warehouses.ToViewModel<WarehouseViewModel>(Mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<WarehouseViewModel>>> Get(int id)
    {
        var warehouse = await Mediator.Send(new GetWarehouseQuery {Id = id});

        return Ok(warehouse.ToViewModel<WarehouseViewModel>(Mapper));
    }
    
    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await Mediator.Send(new DeleteWarehouseCommand() {key = id});
    }
}