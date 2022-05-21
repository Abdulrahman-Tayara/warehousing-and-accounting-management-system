using Application.Common.QueryFilters;
using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.StoragePlaces;

public class GetAllStoragePlacesQuery : GetPaginatedQuery<StoragePlace>
{
    [QueryFilter(QueryFilterCompareType.StringContains)]
    public string? Name { get; set; }
    
    [QueryFilter(QueryFilterCompareType.Equal)]
    public int? WarehouseId { get; set; }

    public bool? IsParent { get; set; }
    
    [QueryFilter(QueryFilterCompareType.Equal)]
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

        return Task.FromResult(query);
    }

    protected override IQueryable<StoragePlace> ApplyFilters(IQueryable<StoragePlace> query, GetAllStoragePlacesQuery request)
    {
        var q = base.ApplyFilters(query, request);
        
        if (request.IsParent != null)
            q = q.Where(storagePlace =>
                (bool) request.IsParent ? storagePlace.ContainerId == null : storagePlace.ContainerId != null);

        return q;
    }
}