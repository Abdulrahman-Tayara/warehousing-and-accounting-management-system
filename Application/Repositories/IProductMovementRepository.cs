using Application.Common.Dtos;
using Domain.Aggregations;
using Domain.Entities;

namespace Application.Repositories;

public interface IProductMovementRepository : IRepositoryCrud<ProductMovement, int>
{
    IQueryable<AggregateProductQuantity> AggregateProductsQuantities(ProductMovementFilters? filters = default);

    IQueryable<AggregateStoragePlaceQuantity> AggregateStoragePlacesQuantities(
        ProductMovementFilters? filters = default);
}