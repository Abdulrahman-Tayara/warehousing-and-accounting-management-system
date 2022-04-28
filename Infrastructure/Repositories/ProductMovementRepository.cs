using Application.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class ProductMovementRepository : IProductMovementRepository
{
    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task<SaveAction<Task<ProductMovement>>> CreateAsync(ProductMovement entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<ProductMovement> GetAll(GetAllOptions<ProductMovement>? options = default)
    {
        throw new NotImplementedException();
    }

    public Task<ProductMovement> FindByIdAsync(int id, FindOptions? options = default)
    {
        throw new NotImplementedException();
    }

    public Task<ProductMovement> Update(ProductMovement entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<AggregateProductQuantity> AggregateProductsQuantities(IList<int> productIds)
    {
        throw new NotImplementedException();
    }

    public Task<SaveAction<Task<IEnumerable<ProductMovement>>>> CreateAllAsync(IEnumerable<ProductMovement> entities)
    {
        throw new NotImplementedException();
    }
}