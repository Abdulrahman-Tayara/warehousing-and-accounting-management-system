using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Currencies;

[Authorize(Method = Method.Delete, Resource = Resource.Currencies)]
public class DeleteCurrencyCommand : DeleteEntityCommand<int>
{

}

public class DeleteCurrencyCommandHandler : DeleteEntityCommandHandler<DeleteCurrencyCommand, Currency, int, ICurrencyRepository>
{
    public DeleteCurrencyCommandHandler(ICurrencyRepository repository) : base(repository)
    {
    }
}