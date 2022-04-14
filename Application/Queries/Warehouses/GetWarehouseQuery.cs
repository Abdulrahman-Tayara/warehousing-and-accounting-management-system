using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Warehouses;

public class GetWarehouseQuery : IRequest<Warehouse>
{
    public int Id { get; set; }
}

public class GetWarehouseQueryHandler : IRequestHandler<GetWarehouseQuery, Warehouse>
{
    private readonly IWarehouseRepository _warehouseRepository;

    public GetWarehouseQueryHandler(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public Task<Warehouse> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
    {
        return _warehouseRepository.FindByIdAsync(request.Id);
    }
}