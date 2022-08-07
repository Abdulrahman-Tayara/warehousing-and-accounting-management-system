using Application.Common.QueryFilters;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.StoragePlaces;

[Authorize(Method = Method.Read, Resource = Resource.Warehouses)]
public class GetStoragePlaceQuery : IRequest<StoragePlace>
{
    public int Id { get; set; }
    
    [QueryFilter(QueryFilterCompareType.StringContains)]
    public string? Name {get; set; }
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