using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Warehouses;

public class GetAllWarehousesQuery : GetPaginatedQuery<Warehouse>
{
    
}

public class GetAllWarehousesQueryHandler : PaginatedQueryHandled<GetAllWarehousesQuery, Warehouse>
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