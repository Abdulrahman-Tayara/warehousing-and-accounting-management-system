using Application.Commands.StoragePlaces;
using Application.Queries.StoragePlaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Pagination;
using wms.Dto.StoragePlaces;

namespace wms.Controllers.Api;

[Authorize]
[Route("api/warehouses/{warehouseId}/places")]
public class StoragePlacesController : ApiControllerBase
{
    public StoragePlacesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<StoragePlaceViewModel>>> Create(int warehouseId,
        CreateStoragePlaceRequest request)
    {
        var command = Mapper.Map<CreateStoragePlaceCommand>(request);
        command.WarehouseId = warehouseId;

        var placeId = await Mediator.Send(command);

        var place = await Mediator.Send(new GetStoragePlaceQuery {Id = placeId});

        return Ok(place.ToViewModel<StoragePlaceViewModel>(Mapper));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<PageViewModel<StoragePlaceViewModel>>>> GetAll(int warehouseId,
        [FromQuery] PaginationRequestParams request)
    {
        var places = await Mediator.Send(
            request.AsQuery(new GetAllStoragePlacesQuery {WarehouseId = warehouseId})
        );

        return Ok(places.ToViewModel<StoragePlaceViewModel>(Mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<StoragePlaceViewModel>>> Get(int id)
    {
        var place = await Mediator.Send(new GetStoragePlaceQuery {Id = id});

        return Ok(place.ToViewModel<StoragePlaceViewModel>(Mapper));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BaseResponse<StoragePlaceViewModel>>> Update(int id, int warehouseId,
        UpdateStoragePlaceRequest request)
    {
        var command = Mapper.Map<UpdateStoragePlaceCommand>(request);
        command.Id = id;
        command.WarehouseId = warehouseId;

        var placeId = await Mediator.Send(command);

        return await Get(placeId);
    }
}