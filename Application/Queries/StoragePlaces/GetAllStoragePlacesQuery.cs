using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.StoragePlaces;

public class GetAllStoragePlacesQuery : IRequest<IEnumerable<StoragePlace>>
{
    public int? WarehouseId { get; set; }
}

public class GetAllStoragePlacesQueryHandler : IRequestHandler<GetAllStoragePlacesQuery, IEnumerable<StoragePlace>>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;

    public GetAllStoragePlacesQueryHandler(IStoragePlaceRepository storagePlaceRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
    }

    public Task<IEnumerable<StoragePlace>> Handle(GetAllStoragePlacesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_storagePlaceRepository.GetAll(new GetAllOptions {IncludeRelations = true}));
    }
}