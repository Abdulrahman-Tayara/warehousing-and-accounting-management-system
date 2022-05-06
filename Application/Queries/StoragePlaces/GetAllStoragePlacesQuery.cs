using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.StoragePlaces;

public class GetAllStoragePlacesQuery : GetPaginatedQuery<StoragePlace>
{
    public int? WarehouseId { get; set; }

    public bool? IsParent { get; set; }
    
    public int? ContainerId { get; set; }
}

public class GetAllStoragePlacesQueryHandler : PaginatedQueryHandler<GetAllStoragePlacesQuery, StoragePlace>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;

    public GetAllStoragePlacesQueryHandler(IStoragePlaceRepository storagePlaceRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
    }

    protected override Task<IQueryable<StoragePlace>> GetQuery(GetAllStoragePlacesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _storagePlaceRepository.GetAll(
            new GetAllOptions<StoragePlace>
            {
                IncludeRelations = true,
            });

        if (request.WarehouseId != null)
            query = query.Where(storagePlace => storagePlace.WarehouseId == request.WarehouseId);
        if (request.IsParent != null)
            query = query.Where(storagePlace =>
                (bool) request.IsParent ? storagePlace.ContainerId == null : storagePlace.ContainerId != null);
        if (request.ContainerId != null)
            query = query.Where(storagePlace => storagePlace.ContainerId == request.ContainerId);

        return Task.FromResult(query);
    }
}