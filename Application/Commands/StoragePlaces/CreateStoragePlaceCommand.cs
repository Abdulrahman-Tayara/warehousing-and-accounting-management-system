using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.StoragePlaces;

[Authorize(Method = Method.Update, Resource = Resource.Warehouses)]
public class CreateStoragePlaceCommand : ICreateEntityCommand<int>
{
    public string Name { get; set; }
    
    public int WarehouseId { get; set; }
    
    public int? ContainerId { get; set; }
    
    public string? Description { get; set; }
}

public class CreateStoragePlaceCommandHandler : CreateEntityCommandHandler<CreateStoragePlaceCommand, StoragePlace, int,
        IStoragePlaceRepository>
{
    public CreateStoragePlaceCommandHandler(IStoragePlaceRepository repository) : base(repository)
    {
    }

    protected override StoragePlace CreateEntity(CreateStoragePlaceCommand request)
    {
        return new StoragePlace
        {
            WarehouseId = request.WarehouseId,
            ContainerId = request.ContainerId,
            Name = request.Name,
            Description = request.Description
        };
    }
}