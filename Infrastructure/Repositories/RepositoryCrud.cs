using System.ComponentModel.DataAnnotations;
using Application.Exceptions;
using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Models;
using Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class RepositoryCrud<TEntity, TModel> : RepositoryCrudBase<ApplicationDbContext, TEntity, int, TModel>
    where TEntity : BaseEntity<int>
    where TModel : class
{
    public RepositoryCrud(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}

public abstract class RepositoryCrudBase<TContext, TEntity, TKey, TModel> : RepositoryBase<TContext>,
    IRepositoryCrud<TEntity, TKey>
    where TContext : DbContext
    where TEntity : BaseEntity<TKey>
    where TModel : class
    where TKey : IEquatable<TKey>
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

    public IEnumerable<TEntity> GetAll()
    {
        return dbSet.AsQueryable()
            .ProjectTo<TEntity>(mapper.ConfigurationProvider);
    }

    protected IEnumerable<TEntity> GetAllFiltered(Func<TModel, bool> filter)
    {
        return dbSet.Where(model => filter(model)).ProjectTo<TEntity>(mapper.ConfigurationProvider);
    }

    public Task<TEntity> GetFirstAsync(Func<TEntity, bool> filter)
    {
        return dbSet.FirstAsync(model => filter(MapModelToEntity(model)))
            .ContinueWith(task => MapModelToEntity(task.Result));
    }

    public async Task<TEntity> FindByIdAsync(TKey id)
    {
        try
        {
            var model = await _findByIdAsync(id);
            return MapModelToEntity(model);
        }
        catch (InvalidOperationException e)
        {
            throw new NotFoundException();
        }
    }

    private  Task<TModel> _findByIdAsync(TKey id)
    {
        return dbSet.FirstAsync(model => model.Id().Equals(id));
    }

    public async Task DeleteAsync(TKey id)
    {
        try
        {
            var model = await _findByIdAsync(id);
            dbSet.Remove(model);
        }
        catch (InvalidOperationException _)
        {
            throw new NotFoundException();
        }
    }

    protected TModel MapEntityToModel(TEntity entity) =>
        mapper.Map<TModel>(entity);

    protected TEntity MapModelToEntity(TModel model) =>
        mapper.Map<TEntity>(model);
}