using Application.Common.QueryFilters;
using Application.Common.Security;
using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Warehouses;

[Authorize(Method = Method.Read, Resource = Resource.Warehouses)]
public class GetAllWarehousesQuery : GetPaginatedQuery<Warehouse>
{
    [QueryFilter(QueryFilterCompareType.StringContains)]
    public string? Name { get; set; }
}

public class GetAllWarehousesQueryHandler : PaginatedQueryHandler<GetAllWarehousesQuery, Warehouse>
{
    private readonly IWarehouseRepository _warehouseRepository;

    public GetAllWarehousesQueryHandler(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    protected override Task<IQueryable<Warehouse>> GetQuery(GetAllWarehousesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_warehouseRepository.GetAll());
    }

}