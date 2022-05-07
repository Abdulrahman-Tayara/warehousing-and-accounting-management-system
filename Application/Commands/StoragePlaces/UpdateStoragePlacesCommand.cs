using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.StoragePlaces;

public class UpdateStoragePlaceCommand : IUpdateEntityCommand<int>
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int WarehouseId { get; set; }
    
    public int? ContainerId { get; set; }
    
    public string? Description { get; set; }
}

public class UpdateStoragePlaceCommandHandler : UpdateEntityCommandHandler<UpdateStoragePlaceCommand, StoragePlace, int,
    IStoragePlaceRepository>
{
    public UpdateStoragePlaceCommandHandler(IStoragePlaceRepository repository) : base(repository)
    {
    }

    protected override StoragePlace GetEntityToUpdate(UpdateStoragePlaceCommand request)
    {
        return new StoragePlace
        {
            Id = request.Id,
            WarehouseId = request.WarehouseId,
            ContainerId = request.ContainerId,
            Name = request.Name,
            Description = request.Description
        };
    }
}