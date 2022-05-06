using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.StoragePlaces;

public class DeleteStoragePlaceCommand : DeleteEntityCommand<int>
{
    
}

public class DeleteStoragePlaceCommandHandler : DeleteEntityCommandHandler<DeleteStoragePlaceCommand, StoragePlace, int, IStoragePlaceRepository>
{
    public DeleteStoragePlaceCommandHandler(IStoragePlaceRepository repository) : base(repository)
    {
    }
}