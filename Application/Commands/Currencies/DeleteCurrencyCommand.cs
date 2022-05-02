using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Currencies;

public class DeleteCurrencyCommand : DeleteEntityCommand<int>
{

}

public class DeleteCurrencyCommandHandler : DeleteEntityCommandHandler<DeleteCurrencyCommand, Currency, int, ICurrencyRepository>
{
    public DeleteCurrencyCommandHandler(ICurrencyRepository repository) : base(repository)
    {
    }
}