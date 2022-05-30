using Application.Common.Models;
using Application.Queries.Common;
using Application.Repositories;
using Domain.Aggregations;
using MediatR;

namespace Application.Queries.StoragePlaces;

public class InventoryStoragePlaceQuery : GetPaginatedQuery<AggregateStoragePlaceQuantity>
{
    public int ProductId { get; }
    
    public int WarehouseId { get; }

    public InventoryStoragePlaceQuery(int productId, int warehouseId)
    {
        ProductId = productId;
        WarehouseId = warehouseId;
    }
}

public class GetStoragePlaceInventoryQueryHandler
    : IRequestHandler<InventoryStoragePlaceQuery, IPaginatedEnumerable<AggregateStoragePlaceQuantity>>
{

    private readonly IProductMovementRepository _productMovementRepository;

    public GetStoragePlaceInventoryQueryHandler(IProductMovementRepository productMovementRepository)
    {
        _productMovementRepository = productMovementRepository;
    }

    public Task<IPaginatedEnumerable<AggregateStoragePlaceQuantity>> Handle(InventoryStoragePlaceQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _productMovementRepository.AggregateStoragePlacesQuantities(request.ProductId, request.WarehouseId)
                .AsPaginatedQuery(request.Page, request.PageSize)
        );
    }
}