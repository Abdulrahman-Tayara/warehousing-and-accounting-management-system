using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.StoragePlaces;

public class GetStoragePlaceQuery : IRequest<StoragePlace>
{
    public int Id { get; set; }
}

public class GetStoragePlaceQueryHandler : IRequestHandler<GetStoragePlaceQuery, StoragePlace>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;

    public GetStoragePlaceQueryHandler(IStoragePlaceRepository storagePlaceRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
    }

    public Task<StoragePlace> Handle(GetStoragePlaceQuery request, CancellationToken cancellationToken)
    {
        return _storagePlaceRepository.FindByIdAsync(request.Id, new FindOptions {IncludeRelations = true});
    }
}