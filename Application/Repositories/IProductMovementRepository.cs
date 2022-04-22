
using Domain.Entities;

namespace Application.Repositories;

public interface IProductMovementRepository : IRepositoryCrud<ProductMovement, int>
{
    IQueryable<AggregatedProductQuantity> AggregateProductsQuantities(IList<int> productIds);
}

public class AggregatedProductQuantity
{
    public Product Product { get; set; }
    public int QuantitySum { get; set; }
}