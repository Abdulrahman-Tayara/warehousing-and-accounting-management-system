using Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class RepositoryBase<TContext> : IRepositoryBase where TContext : DbContext
{
    protected readonly TContext dbContext;

    public RepositoryBase(TContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task SaveChanges()
    {
        return dbContext.SaveChangesAsync();
    }
}