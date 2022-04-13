using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Common;

public interface ICreateEntityCommand<out TKey> : IRequest<TKey>
{
    
}

public abstract class CreateEntityCommandHandler<TRequest, TEntity, TKey, TRepository> : IRequestHandler<TRequest, TKey>
    where TRequest : ICreateEntityCommand<TKey>
    where TEntity : BaseEntity<TKey>
    where TRepository : IRepositoryCrud<TEntity, TKey>
{

    protected readonly TRepository Repository;

    public CreateEntityCommandHandler(TRepository repository)
    {
        Repository = repository;
    }

    public async Task<TKey> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var entity = CreateEntity(request);

        var saveAction = await Repository.CreateAsync(entity);
        
        entity = await saveAction();
        
        return entity.Id;
    }

    protected abstract TEntity CreateEntity(TRequest request);
}