
using Domain.Entities;

namespace Application.Repositories;

public interface IProductMovementRepository : IRepositoryCrud<ProductMovement, int>
{
    IQueryable<AggregateProductQuantity> AggregateProductsQuantities(IList<int> productIds);
}