using Application.Exceptions;
using Domain.Entities;

namespace Application.Repositories;

public interface IRepositoryBase
{
    Task SaveChanges();
}

public interface IRepositoryCrud<TEntity, TKey> : IRepositoryBase where TEntity : BaseEntity<TKey>
{
    Task<TEntity> CreateAsync(TEntity entity);

    IEnumerable<TEntity> GetAll();

    
    /// <exception cref="NotFoundException"></exception>
    Task<TEntity> FindByIdAsync(TKey id);

    Task DeleteAsync(TKey id);
}