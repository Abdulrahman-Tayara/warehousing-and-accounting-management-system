using Application.Common.Dtos;
using Application.Queries.StoragePlaces;
using Application.Queries.Warehouses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Pagination;
using wms.Dto.ProductQuantity;
using wms.Dto.StoragePlaceQuantity;

namespace wms.Controllers.Api;

[Authorize]
public class InventoryController : ApiControllerBase
{
    public InventoryController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet("byProduct")]
    public async Task<ActionResult<BaseResponse<PageViewModel<ProductQuantityViewModel>>>> InventoryWarehouse(
        [FromQuery] PaginationRequestParams paginationParams,
        [FromQuery] ProductMovementFilters filters)
    {
        var query = paginationParams.AsQuery(new InventoryWarehouseQuery {Filters = filters});

        var productQuantities = await Mediator.Send(query);

        return Ok(productQuantities.ToViewModel<ProductQuantityViewModel>(Mapper));
    }

    [HttpGet("byProductAndStoragePlace")]
    public async Task<ActionResult<BaseResponse<PageViewModel<StoragePlaceQuantityViewModel>>>> Inventory(
        [FromQuery] PaginationRequestParams paginationParams,
        [FromQuery] ProductMovementFilters filters
    )
    {
        var query = paginationParams.AsQuery(new InventoryStoragePlaceQuery(filters));

        var storagePlaceQuantities = await Mediator.Send(query);

        return Ok(storagePlaceQuantities.ToViewModel<StoragePlaceQuantityViewModel>(Mapper));
    }
}