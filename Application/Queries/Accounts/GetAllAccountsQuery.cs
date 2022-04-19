using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Accounts;

public class GetAllAccountsQuery : GetPaginatedQuery<Account>
{
}

public class GetAllAccountsQueryHandler : PaginatedQueryHandler<GetAllAccountsQuery, Account>
{
    private readonly IAccountRepository _repository;

    public GetAllAccountsQueryHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    protected override Task<IQueryable<Account>> GetQuery(GetAllAccountsQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetAll());
    }
}