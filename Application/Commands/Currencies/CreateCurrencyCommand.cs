using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Currencies;

public class CreateCurrencyCommand : ICreateEntityCommand<int>
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    public float Factor { get; set; }
}

public class CreateCurrencyCommandHandler : CreateEntityCommandHandler<CreateCurrencyCommand, Currency, int, ICurrencyRepository>
{
    public CreateCurrencyCommandHandler(ICurrencyRepository repository) : base(repository)
    {
    }

    protected override Currency CreateEntity(CreateCurrencyCommand request)
    {
        return new Currency
        {
            Name = request.Name,
            Symbol = request.Symbol,
            Factor = request.Factor
        };
    }
}