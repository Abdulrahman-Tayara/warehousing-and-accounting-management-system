using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Accounts;

public class DeleteAccountCommand : DeleteEntityCommand<int>
{
}

public class DeleteAccountCommandHandler : DeleteEntityCommandHandler<DeleteAccountCommand, Account, int, IAccountRepository>
{
    public DeleteAccountCommandHandler(IAccountRepository repository) : base(repository)
    {
    }
}