using Application.Common.QueryFilters;
using Application.Common.Security;
using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Currencies;

[Authorize(Method = Method.Read, Resource = Resource.Currencies)]
public class GetAllCurrenciesQuery : GetPaginatedQuery<Currency>
{
    [QueryFilter(QueryFilterCompareType.StringContains)]
    public string? Name {get; set; }
}

public class GetAllCurrenciesQueryHandler : PaginatedQueryHandler<GetAllCurrenciesQuery, Currency>
{

    private readonly ICurrencyRepository _currencyRepository;

    public GetAllCurrenciesQueryHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    protected override Task<IQueryable<Currency>> GetQuery(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_currencyRepository.GetAll());
    }
}