using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Currencies;

public class GetAllCurrenciesQuery : IRequest<IEnumerable<Currency>>
{
    
}

public class GetAllCurrenciesQueryHandler : RequestHandler<GetAllCurrenciesQuery, IEnumerable<Currency>>
{

    private readonly ICurrencyRepository _currencyRepository;

    public GetAllCurrenciesQueryHandler(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    protected override IEnumerable<Currency> Handle(GetAllCurrenciesQuery request)
    {
        return _currencyRepository.GetAll();
    }
}