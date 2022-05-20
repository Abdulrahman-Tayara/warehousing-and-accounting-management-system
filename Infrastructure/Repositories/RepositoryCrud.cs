using Application.Exceptions;
using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
    protected readonly IMapper Mapper;
    protected readonly DbSet<TModel> DbSet;

    public RepositoryCrudBase(TContext dbContext, IMapper mapper) : base(dbContext)
    {
        this.Mapper = mapper;
        DbSet = dbContext.Set<TModel>();
    }

    public async Task<SaveAction<Task<TEntity>>> CreateAsync(TEntity entity)
    {
        var model = MapEntityToModel(entity);

        var result = await DbSet.AddAsync(model);

        return async () =>
        {
            await SaveChanges();

            return MapModelToEntity(result.Entity);
        };
    }

    public async Task<SaveAction<Task<IEnumerable<TEntity>>>> CreateAllAsync(IEnumerable<TEntity> entities)
    {
        var models = entities.Select(MapEntityToModel);


        IList<EntityEntry<TModel>> results = new List<EntityEntry<TModel>>();
        foreach (var model in models)
        {
            results.Add(await DbSet.AddAsync(model));
        }

        return async () =>
        {
            await SaveChanges();

            return results.Select(res => MapModelToEntity(res.Entity));
        };
    }

    public IQueryable<TEntity> GetAll(GetAllOptions<TEntity>? options = default)
    {
        IQueryable<TModel> set = options is {IncludeRelations: true} ? GetIncludedDbSet() : DbSet;
        
        IQueryable<TEntity> entitiesSet = set.ProjectTo<TEntity>(Mapper.ConfigurationProvider);

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
        IQueryable<TModel> set = options is {IncludeRelations: true} ? GetIncludedDbSet() : DbSet;
        
        return set.FirstAsync(model => model.Id.Equals(id));
    }

    public Task<bool> IsExistsById(TKey id)
    {
        return DbSet.AnyAsync(model => model.Id.Equals(id));
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
            DbSet.Remove(model);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.StackTrace);
            throw new NotFoundException();
        }
    }

    protected TModel MapEntityToModel(TEntity entity) =>
        Mapper.Map<TModel>(entity);

    protected TEntity MapModelToEntity(TModel model) =>
        Mapper.Map<TEntity>(model);

    protected virtual IQueryable<TModel> GetIncludedDbSet() => DbSet.AsQueryable();
}