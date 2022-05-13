using System.Linq.Expressions;
using Application.Exceptions;
using Domain.Entities;

namespace Application.Repositories;

public interface IRepositoryBase
{
    Task SaveChanges();
}

public interface IRepositoryCrud<TEntity, TKey> : IRepositoryBase where TEntity : BaseEntity<TKey>
{
    Task<SaveAction<Task<TEntity>>> CreateAsync(TEntity entity);

    Task<SaveAction<Task<IEnumerable<TEntity>>>> CreateAllAsync(IEnumerable<TEntity> entities);

    IQueryable<TEntity> GetAll(GetAllOptions<TEntity>? options = default);


    /// <exception cref="NotFoundException"></exception>
    Task<TEntity> FindByIdAsync(TKey id, FindOptions? options = default);

    Task<bool> IsExistsById(TKey id);

    Task<TEntity> Update(TEntity entity);

    Task DeleteAsync(TKey id);
}

public delegate T SaveAction<T>();

public class FindOptions
{
    public bool IncludeRelations { get; set; } = false;
}

public class GetAllOptions<TEntity>
{
    public bool IncludeRelations { get; set; } = false;
}