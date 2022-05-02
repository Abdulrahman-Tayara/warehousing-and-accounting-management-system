using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Common;

public class DeleteEntityCommand<TKey> : IRequest
{
    public TKey key;
}

public abstract class
    DeleteEntityCommandHandler<TRequest, TEntity, TKey, TRepository> : AsyncRequestHandler<TRequest>
    where TRequest : DeleteEntityCommand<TKey>
    where TEntity : BaseEntity<TKey>
    where TRepository : IRepositoryCrud<TEntity, TKey>
{
    private readonly TRepository _repository;

    protected DeleteEntityCommandHandler(TRepository repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(TRequest request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.key);
    }
}