using Application.Common.QueryFilters;
using Application.Common.Security;
using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.CountryOrigins;

[Authorize(Method = Method.Read, Resource = Resource.Countries)]
public class GetAllCountryOriginsQuery : GetPaginatedQuery<CountryOrigin>
{
    [QueryFilter(QueryFilterCompareType.StringContains)]
    public string? Name {get; set; }
}

public class GetAllCountryOriginsQueryHandler : PaginatedQueryHandler<GetAllCountryOriginsQuery, CountryOrigin>
{

    private readonly ICountryOriginRepository _repository;
    
    public GetAllCountryOriginsQueryHandler(ICountryOriginRepository repository)
    {
        _repository = repository;
    }

    protected override Task<IQueryable<CountryOrigin>> GetQuery(GetAllCountryOriginsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetAll());
    }
}