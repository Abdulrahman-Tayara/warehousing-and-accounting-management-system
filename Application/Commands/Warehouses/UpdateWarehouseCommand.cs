using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Warehouses;

public class UpdateWarehouseCommand : IUpdateEntityCommand<int>
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Location { get; set; }
}

public class UpdateWarehouseCommandHandler : UpdateEntityCommandHandler<UpdateWarehouseCommand, Warehouse, int,
        IWarehouseRepository>
{
    public UpdateWarehouseCommandHandler(IWarehouseRepository repository) : base(repository)
    {
    }

    protected override Warehouse GetEntityToUpdate(UpdateWarehouseCommand request)
    {
        return new Warehouse
        {
            Id = request.Id,
            Name = request.Name,
            Location = request.Location
        };
    }
}