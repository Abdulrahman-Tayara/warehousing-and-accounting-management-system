using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Warehouses;

public class GetAllWarehousesQuery : IRequest<IEnumerable<Warehouse>>
{
    
}

public class GetAllWarehousesQueryHandler : IRequestHandler<GetAllWarehousesQuery, IEnumerable<Warehouse>>
{
    private readonly IWarehouseRepository _warehouseRepository;

    public GetAllWarehousesQueryHandler(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public Task<IEnumerable<Warehouse>> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_warehouseRepository.GetAll());
    }
}