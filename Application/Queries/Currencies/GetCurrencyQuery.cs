using Application.Common.QueryFilters;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Currencies;

public class GetCurrencyQuery : IRequest<Currency>
{
    public int Id { get; set; }
    
    [QueryFilter(QueryFilterCompareType.StringContains)]
    public string? Name {get; set; }
}

public class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery, Currency>
{
    private readonly ICurrencyRepository _currencyRepository;

    public GetCurrencyQueryHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public Task<Currency> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
    {
        return _currencyRepository.FindByIdAsync(request.Id);
    }
}