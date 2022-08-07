using Application.Common.Security;
using Application.Repositories;
using Domain.Factories;
using MediatR;

namespace Application.Commands.Journals;

[Authorize(Method = Method.Write, Resource = Resource.Journals)]
public class CreateJournalsCommand : IRequest<(int DebitId, int CreditId)>
{
    public int SourceAccountId { get; }

    public int DestinationAccountId { get; }

    public double Value { get; }
    
    public int CurrencyId { get; }

    public CreateJournalsCommand(int sourceAccountId, int destinationAccountId, double value, int currencyId)
    {
        SourceAccountId = sourceAccountId;
        DestinationAccountId = destinationAccountId;
        Value = value;
        CurrencyId = currencyId;
    }
}

public class CreateJournalsCommandHandler : IRequestHandler<CreateJournalsCommand, (int DebitId, int CreditId)>
{
    private readonly IJournalRepository _journalRepository;

    public CreateJournalsCommandHandler(IJournalRepository journalRepository)
    {
        _journalRepository = journalRepository;
    }

    public async Task<(int DebitId, int CreditId)> Handle(CreateJournalsCommand request,
        CancellationToken cancellationToken)
    {
        var (debit, credit) =
            new JournalsFactory().CreateJournals(request.SourceAccountId, request.DestinationAccountId, request.Value, request.CurrencyId);

        var saveAction = await _journalRepository.CreateJournals(debit, credit);

        var (createdDebit, createdCredit) = await saveAction();

        return (createdDebit.Id, createdCredit.Id);
    }
}