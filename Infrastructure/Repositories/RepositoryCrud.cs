using Application.Exceptions;
using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class RepositoryCrud<TEntity, TModel> : RepositoryCrudBase<ApplicationDbContext, TEntity, int, TModel>
    where TEntity : BaseEntity<int>
    where TModel : class, IDbModel
{
    public RepositoryCrud(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}

public abstract class RepositoryCrudBase<TContext, TEntity, TKey, TModel> : RepositoryBase<TContext>,
    IRepositoryCrud<TEntity, TKey>
    where TContext : DbContext
    where TEntity : BaseEntity<TKey>
    where TModel : class, IDbModel
    where TKey : IEquatable<TKey>
{
    protected readonly IMapper mapper;
    protected readonly DbSet<TModel> dbSet;

    public RepositoryCrudBase(TContext dbContext, IMapper mapper) : base(dbContext)
    {
        this.mapper = mapper;
        dbSet = dbContext.Set<TModel>();
    }

    public async Task<SaveAction<Task<TEntity>>> CreateAsync(TEntity entity)
    {
        var model = MapEntityToModel(entity);

        var result = await dbSet.AddAsync(model);

        return async () =>
        {
            await SaveChanges();
        
            return MapModelToEntity(result.Entity);
        };
    }

    public IEnumerable<TEntity> GetAll(GetAllOptions<TEntity>? options = default)
    {
        IQueryable<TModel> set = options is {IncludeRelations: true} ? GetIncludedDbSet() : dbSet;

        IQueryable<TEntity> entitiesSet =  set.ProjectTo<TEntity>(mapper.ConfigurationProvider);

        if (options is {Filter: { }})
        {
            entitiesSet = entitiesSet.Where(options.Filter);
        }

        return entitiesSet;
    }
    
    public async Task<TEntity> FindByIdAsync(TKey id, FindOptions? options = default)
    {
        try
        {
            var model = await _findByIdAsync(id, options);
            return MapModelToEntity(model);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.StackTrace);
            throw new NotFoundException();
        }
    }

    private Task<TModel> _findByIdAsync(TKey id, FindOptions? options = default)
    {
        IQueryable<TModel> set = options is {IncludeRelations: true} ? GetIncludedDbSet() : dbSet;
        
        return set.FirstAsync(model => model.Id.Equals(id));
    }
    
    public async Task<TEntity> Update(TEntity entity)
    {
        try
        {
            var modelInDb = await _findByIdAsync(entity.Id);

            dbContext.Entry(modelInDb).CurrentValues.SetValues(entity);
            
            return entity;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.StackTrace);
            throw new NotFoundException();
        }
    }

    public async Task DeleteAsync(TKey id)
    {
        try
        {
            var model = await _findByIdAsync(id);
            dbSet.Remove(model);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.StackTrace);
            throw new NotFoundException();
        }
    }

    protected TModel MapEntityToModel(TEntity entity) =>
        mapper.Map<TModel>(entity);

    protected TEntity MapModelToEntity(TModel model) =>
        mapper.Map<TEntity>(model);

    protected virtual IQueryable<TModel> GetIncludedDbSet() => dbSet.AsQueryable();
}