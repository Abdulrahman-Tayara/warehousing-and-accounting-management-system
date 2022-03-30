using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class RepositoryCrud<TEntity, TModel> : RepositoryCrudBase<ApplicationDbContext, TEntity, int, TModel>
    where TEntity : BaseEntity<int>
    where TModel : class
{
    public RepositoryCrud(ApplicationDbContext dbContext, Mapper mapper) : base(dbContext, mapper)
    {
    }
}

public abstract class RepositoryCrudBase<TContext, TEntity, TKey, TModel> : RepositoryBase<TContext>,
    IRepositoryCrud<TEntity, TKey>
    where TContext : DbContext
    where TEntity : BaseEntity<TKey>
    where TModel : class
{
    protected readonly IMapper mapper;
    protected readonly DbSet<TModel> dbSet;

    public RepositoryCrudBase(TContext dbContext, IMapper mapper) : base(dbContext)
    {
        this.mapper = mapper;
        dbSet = dbContext.Set<TModel>();
    }

    public Task<TEntity> CreateAsync(TEntity entity)
    {
        var model = MapEntityToModel(entity);

        var result = dbSet.AddAsync(model);

        return result.AsTask()
            .ContinueWith(task => MapModelToEntity(task.Result.Entity));
    }

    public IEnumerable<TEntity> GetAllAsync(Func<TEntity, bool>? filter = default)
    {
        if (filter == null)
        {
            return dbSet.AsQueryable()
                .ProjectTo<TEntity>(mapper.ConfigurationProvider);
        }

        return dbSet.Where(model => filter(MapModelToEntity(model)))
            .ProjectTo<TEntity>(mapper.ConfigurationProvider);
    }

    protected TModel MapEntityToModel(TEntity entity) =>
        mapper.Map<TModel>(entity);

    protected TEntity MapModelToEntity(TModel model) =>
        mapper.Map<TEntity>(model);
}