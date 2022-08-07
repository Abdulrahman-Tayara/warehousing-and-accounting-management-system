using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.StoragePlaces;

[Authorize(Method = Method.Update, Resource = Resource.Warehouses)]
public class DeleteStoragePlaceCommand : DeleteEntityCommand<int>
{
    
}

public class DeleteStoragePlaceCommandHandler : DeleteEntityCommandHandler<DeleteStoragePlaceCommand, StoragePlace, int, IStoragePlaceRepository>
{
    public DeleteStoragePlaceCommandHandler(IStoragePlaceRepository repository) : base(repository)
    {
    }
}