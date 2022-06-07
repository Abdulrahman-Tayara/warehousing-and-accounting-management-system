using Application.Repositories;
using Application.Settings;
using Domain.Aggregations;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Accounting;

public class GetAccountStatementQuery : IRequest<AggregateAccountStatement>
{
    public int AccountId { get; }

    public GetAccountStatementQuery(int accountId)
    {
        AccountId = accountId;
    }
}

public class GetAccountStatementQueryHandler : IRequestHandler<GetAccountStatementQuery, AggregateAccountStatement>
{
    private readonly IJournalRepository _journalRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ICurrencyRepository _currencyRepository;

    private readonly ApplicationSettings _applicationSettings;

    public GetAccountStatementQueryHandler(
        IJournalRepository journalRepository,
        IAccountRepository accountRepository,
        ICurrencyRepository currencyRepository,
        ApplicationSettings applicationSettings
    )
    {
        _journalRepository = journalRepository;
        _accountRepository = accountRepository;
        _currencyRepository = currencyRepository;
        _applicationSettings = applicationSettings;
    }

    public async Task<AggregateAccountStatement> Handle(GetAccountStatementQuery request,
        CancellationToken cancellationToken)
    {
        var detailEntries = _journalRepository.GetAll()
            .Where(journal => journal.SourceAccountId == request.AccountId)
            .GroupBy(
                journal => new
                {
                    journal.SourceAccountId,
                    journal.AccountId,
                    journal.CurrencyId,
                },
                journal => new {journal.Debit, journal.Credit},
                (key, entries) => new
                {
                    key.AccountId,
                    Debit = entries.Sum(j => j.Debit),
                    Credit = entries.Sum(j => j.Credit),
                    key.CurrencyId
                }
            ).ToList();

        var detailsAccounts = _accountRepository.GetAll()
            .Where(account => detailEntries.Select(d => d.AccountId).Contains(account.Id))
            .ToList();

        var currency = await _currencyRepository.FindByIdAsync(_applicationSettings.DefaultCurrencyId);

        var details = detailEntries
            .Zip(detailsAccounts)
            .Select(entry =>
            {
                var (detail, account) = entry;
                return new AggregateAccountStatementDetail(
                    account: account,
                    debit: detail.Debit,
                    credit: detail.Credit,
                    currency: currency
                );
            })
            .ToList();

        var sourceAccount = await _accountRepository.FindByIdAsync(request.AccountId);

        var statement = new AggregateAccountStatement(
            sourceAccount,
            details,
            details.Sum(d => d.Debit),
            details.Sum(d => d.Credit),
            currency
        );

        return statement;
    }
}