using Application.Common.Dtos;
using Application.Common.Models;
using Application.Common.Security;
using Application.Queries.Common;
using Application.Repositories;
using Domain.Aggregations;
using MediatR;

namespace Application.Queries.Warehouses;

[Authorize(Method = Method.Read, Resource = Resource.Invoices)]
public class InventoryWarehouseQuery : GetPaginatedQuery<AggregateProductQuantity>
{
    public ProductMovementFilters? Filters { get; set; }
}

public class InventoryWarehouseQueryHandler : IRequestHandler<InventoryWarehouseQuery,
    IPaginatedEnumerable<AggregateProductQuantity>>
{
    private readonly IProductMovementRepository _productMovementRepository;

    public InventoryWarehouseQueryHandler(IProductMovementRepository productMovementRepository)
    {
        _productMovementRepository = productMovementRepository;
    }

    public Task<IPaginatedEnumerable<AggregateProductQuantity>> Handle(InventoryWarehouseQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _productMovementRepository.AggregateProductsQuantities(request.Filters)
                .AsPaginatedQuery(request.Page, request.PageSize)
        );
    }
}