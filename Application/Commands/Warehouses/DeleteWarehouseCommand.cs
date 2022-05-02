using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Warehouses;

public class DeleteWarehouseCommand : DeleteEntityCommand<int>
{
    
}

public class DeleteWarehouseCommandHandler : DeleteEntityCommandHandler<DeleteWarehouseCommand, Warehouse, int, IWarehouseRepository>
{
    public DeleteWarehouseCommandHandler(IWarehouseRepository repository) : base(repository)
    {
    }
}
