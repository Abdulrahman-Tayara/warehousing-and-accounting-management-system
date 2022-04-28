using Application.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class StoragePlaceRepository : IStoragePlaceRepository
{
    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task<SaveAction<Task<StoragePlace>>> CreateAsync(StoragePlace entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<StoragePlace> GetAll(GetAllOptions<StoragePlace>? options = default)
    {
        throw new NotImplementedException();
    }

    public Task<StoragePlace> FindByIdAsync(int id, FindOptions? options = default)
    {
        throw new NotImplementedException();
    }

    public Task<StoragePlace> Update(StoragePlace entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<SaveAction<Task<IEnumerable<StoragePlace>>>> CreateAllAsync(IEnumerable<StoragePlace> entities)
    {
        throw new NotImplementedException();
    }
}