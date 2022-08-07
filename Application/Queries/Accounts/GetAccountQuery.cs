using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Accounts;

[Authorize(Method = Method.Read, Resource = Resource.Accounts)]
public class GetAccountQuery : IRequest<Account>
{
    public int Id { get; set; }
}

public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, Account>
{
    private readonly IAccountRepository _repository;

    public GetAccountQueryHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public Task<Account> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        => _repository.FindByIdAsync(request.Id);
}