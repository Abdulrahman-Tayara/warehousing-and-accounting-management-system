using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Currencies;

public class GetAllCurrenciesQuery : GetPaginatedQuery<Currency>
{
    
}

public class GetAllCurrenciesQueryHandler : PaginatedQueryHandled<GetAllCurrenciesQuery, Currency>
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