using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.CountryOrigins;

public class GetAllCountryOriginsQuery : GetPaginatedQuery<CountryOrigin>
{
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