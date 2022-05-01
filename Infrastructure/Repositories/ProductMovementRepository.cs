using Application.Repositories;
using AutoMapper;
using Domain.Aggregations;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductMovementRepository : RepositoryCrud<ProductMovement, ProductMovementDb>, IProductMovementRepository
{
    public ProductMovementRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public IQueryable<AggregateProductQuantity> AggregateProductsQuantities(IList<int> productIds)
    {
        return dbSet
            .Include(movement => movement.Product)
            .Select(movement => new
                {movement.Id, movement.ProductId, movement.Product, movement.Quantity, movement.Type})
            .Where(movement => movement.ProductId != null && productIds.Contains((int) movement.ProductId!))
            .GroupBy(movement => movement.ProductId)
            .Select(movementsGrouping => new AggregateProductQuantity()
            {
                Product = mapper.Map<Product>(movementsGrouping.FirstOrDefault()!.Product),
                QuantitySum = movementsGrouping.Sum(movement =>
                    movement.Type == ProductMovementType.In ? movement.Quantity : -movement.Quantity)
            });
    }

    protected override IQueryable<ProductMovementDb> GetIncludedDbSet()
    {
        return dbSet.Include(item =>
                item.CurrencyAmounts!.Where(c => c.Key.Equals(CurrencyAmountKey.Movement)))
            .ThenInclude(currencyAmount => currencyAmount.Currency)
            .Include(item => item.Product)
            .Include(item => item.Currency);
    }
}