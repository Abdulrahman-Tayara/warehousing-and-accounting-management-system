using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Accounts;

[Authorize(Method = Method.Delete, Resource = Resource.Accounts)]
public class DeleteAccountCommand : DeleteEntityCommand<int>
{
}

public class DeleteAccountCommandHandler : DeleteEntityCommandHandler<DeleteAccountCommand, Account, int, IAccountRepository>
{
    public DeleteAccountCommandHandler(IAccountRepository repository) : base(repository)
    {
    }
}