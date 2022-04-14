using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Accounts;

public class GetAllAccountsQuery : IRequest<IEnumerable<Account>>
{
}

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, IEnumerable<Account>>
{
    private readonly IAccountRepository _repository;

    public GetAllAccountsQueryHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Account>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        => Task.FromResult(_repository.GetAll());
}