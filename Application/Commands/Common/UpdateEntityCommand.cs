using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Common;

public interface IUpdateEntityCommand<out TKey> : IRequest<TKey>
{
    
}

public abstract class UpdateEntityCommandHandler<TRequest, TEntity, TKey, TRepository> : IRequestHandler<TRequest, TKey>
    where TRequest : IUpdateEntityCommand<TKey>
    where TEntity : BaseEntity<TKey>
    where TRepository : IRepositoryCrud<TEntity, TKey>
{

    protected readonly TRepository Repository;

    protected UpdateEntityCommandHandler(TRepository repository)
    {
        Repository = repository;
    }

    public async Task<TKey> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var entity = GetEntityToUpdate(request);

        await Repository.Update(entity);

        await Repository.SaveChanges();

        return entity.Id;
    }

    protected abstract TEntity GetEntityToUpdate(TRequest request);
}

