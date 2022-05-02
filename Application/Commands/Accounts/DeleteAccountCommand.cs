using Application.Repositories;
using MediatR;

namespace Application.Commands.Accounts;

public class DeleteAccountCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteAccountCommandHandler : AsyncRequestHandler<DeleteAccountCommand>
{
    private readonly IAccountRepository _accountRepository;

    public DeleteAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    protected override async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        await _accountRepository.DeleteAsync(request.Id);
        await _accountRepository.SaveChanges();
    }
}