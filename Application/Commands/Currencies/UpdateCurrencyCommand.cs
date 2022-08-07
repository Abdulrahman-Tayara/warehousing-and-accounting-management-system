using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Currencies;

[Authorize(Method = Method.Update, Resource = Resource.Currencies)]
public class UpdateCurrencyCommand : IUpdateEntityCommand<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public float Factor { get; set; }
}

public class
    UpdateCurrencyCommandHandler : UpdateEntityCommandHandler<UpdateCurrencyCommand, Currency, int, ICurrencyRepository>
{
    public UpdateCurrencyCommandHandler(ICurrencyRepository repository) : base(repository)
    {
    }

    protected override Currency GetEntityToUpdate(UpdateCurrencyCommand request)
    {
        return new Currency
        {
            Id = request.Id,
            Name = request.Name,
            Factor = request.Factor,
            Symbol = request.Symbol,
        };
    }
}