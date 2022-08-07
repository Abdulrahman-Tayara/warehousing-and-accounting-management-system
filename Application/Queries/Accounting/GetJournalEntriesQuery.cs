using Application.Common.Security;
using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Accounting;

[Authorize(Method = Method.Read, Resource = Resource.Journals)]
public class GetJournalEntriesQuery : GetPaginatedQuery<Journal>
{
    public int FromAccountId { get; set; }

    public int ToAccountId { get; set; }
}

public class GetJournalEntriesHandler : PaginatedQueryHandler<GetJournalEntriesQuery, Journal>
{
    private readonly IJournalRepository _journalRepository;

    public GetJournalEntriesHandler(IJournalRepository journalRepository)
    {
        _journalRepository = journalRepository;
    }

    protected override Task<IQueryable<Journal>> GetQuery(GetJournalEntriesQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _journalRepository.GetAll()
                .Where(journal => journal.SourceAccountId == request.FromAccountId || request.FromAccountId == default)
                .Where(journal => journal.AccountId == request.ToAccountId || request.ToAccountId == default)
        );
    }
}