using Application.Common.Dtos;
using Application.Common.Models;
using Application.Common.Security;
using Application.Queries.Common;
using Application.Repositories;
using Domain.Aggregations;
using MediatR;

namespace Application.Queries.StoragePlaces;

[Authorize(Method = Method.Read, Resource = Resource.Invoices)]
public class InventoryStoragePlaceQuery : GetPaginatedQuery<AggregateStoragePlaceQuantity>
{
    public ProductMovementFilters? Filters { get; set; }

    public InventoryStoragePlaceQuery(ProductMovementFilters? filters)
    {
        Filters = filters;
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

    public Task<IPaginatedEnumerable<AggregateStoragePlaceQuantity>> Handle(InventoryStoragePlaceQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _productMovementRepository.AggregateStoragePlacesQuantities(request.Filters)
                .AsPaginatedQuery(request.Page, request.PageSize)
        );
    }
}