using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.StoragePlaces;

public class CreateStoragePlaceCommand : ICreateEntityCommand<int>
{
    public string Name { get; set; }
    
    public int WarehouseId { get; set; }
    
    public int? ContainerId { get; set; }
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
            Name = request.Name
        };
    }
}