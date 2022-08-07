using Application.Common.QueryFilters;
using Application.Common.Security;
using Application.Queries.Common;
using Application.Repositories;
using Unit = Domain.Entities.Unit;

namespace Application.Queries.Units;

[Authorize(Method = Method.Read, Resource = Resource.Units)]
public class GetAllUnitsQuery : GetPaginatedQuery<Unit>
{
    [QueryFilter(QueryFilterCompareType.StringContains)]
    public string? Name {get; set; }
}

public class GetAllUnitsQueryHandler : PaginatedQueryHandler<GetAllUnitsQuery, Unit>
{

    private readonly IUnitRepository _repository;
    
    public GetAllUnitsQueryHandler(IUnitRepository repository)
    {
        _repository = repository;
    }

    protected override Task<IQueryable<Unit>> GetQuery(GetAllUnitsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetAll());
    }
}