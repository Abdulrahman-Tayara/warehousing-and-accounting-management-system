using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.StoragePlaces;

public class GetAllStoragePlacesQuery : GetPaginatedQuery<StoragePlace>
{
    public int? WarehouseId { get; set; }
}

public class GetAllStoragePlacesQueryHandler : PaginatedQueryHandled<GetAllStoragePlacesQuery, StoragePlace>
{
    private readonly IStoragePlaceRepository _storagePlaceRepository;

    public GetAllStoragePlacesQueryHandler(IStoragePlaceRepository storagePlaceRepository)
    {
        _storagePlaceRepository = storagePlaceRepository;
    }

    protected override Task<IQueryable<StoragePlace>> GetQuery(GetAllStoragePlacesQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(_storagePlaceRepository.GetAll(
            new GetAllOptions<StoragePlace>
            {
                IncludeRelations = true,
                Filter = entity => entity.WarehouseId == request.WarehouseId
            }));
    }
}