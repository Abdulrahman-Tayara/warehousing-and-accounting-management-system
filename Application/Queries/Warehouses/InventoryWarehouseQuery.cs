using Application.Common.Dtos;
using Application.Common.Models;
using Application.Queries.Common;
using Application.Repositories;
using Domain.Aggregations;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Warehouses;

public class InventoryWarehouseQuery : GetPaginatedQuery<AggregateProductQuantity>
{
    public ProductMovementFilters? Filters { get; set; }
}

public class InventoryWarehouseQueryHandler : IRequestHandler<InventoryWarehouseQuery,
    IPaginatedEnumerable<AggregateProductQuantity>>
{
    private readonly IProductMovementRepository _productMovementRepository;
    private readonly IProductRepository _productRepository;

    public InventoryWarehouseQueryHandler(IProductMovementRepository productMovementRepository,
        IProductRepository productRepository)
    {
        _productMovementRepository = productMovementRepository;
        _productRepository = productRepository;
    }

    public Task<IPaginatedEnumerable<AggregateProductQuantity>> Handle(InventoryWarehouseQuery request,
        CancellationToken cancellationToken)
    {
        var aggregates = _productMovementRepository
            .AggregateProductsQuantities(request.Filters)
            .ToList();

        var productIds = aggregates
            .Select(aggregate => aggregate.Product!.Id);

        var products = _productRepository
            .GetAll(new GetAllOptions<Product> {IncludeRelations = true})
            .Where(product => productIds.Any(id => product.Id == id))
            .ToList();

        var aggregatesWithFullProduct = aggregates
            .Zip(products)
            .Select(entry => entry.First.AddProduct(entry.Second));

        var paginatedQuery = aggregatesWithFullProduct
            .AsQueryable()
            .AsPaginatedQuery(request.Page, request.PageSize);

        return Task.FromResult(paginatedQuery);
    }
}