using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : RepositoryCrud<Product, ProductDb>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public IQueryable<Product> GetAllWithNewMinLevelWarnings(int invoiceId)
    {
        var invoiceMovements = dbContext.ProductMovements
            .Where(movement => movement.InvoiceId == invoiceId)
            .ToList();

        var invoiceProductIds = invoiceMovements
            .Select(movement => movement.ProductId.GetValueOrDefault())
            .ToList();

        var invoiceProductIdsAndQuantities = dbContext.ProductMovements
            .Where(movement => invoiceProductIds.Any(productId => productId == movement.ProductId.GetValueOrDefault()))
            .GroupBy(
                movement => movement.ProductId.GetValueOrDefault(),
                m => m.Type == ProductMovementType.In ? m.Quantity : -m.Quantity,
                (productId, quantities) => new {ProductId = productId, quantity = quantities.Sum()}
            )
            .ToList();

        var invoiceProductIdsAndQuantitiesBeforeInvoice = invoiceProductIdsAndQuantities
            .Zip(invoiceMovements)
            .Select(entry => new
            {
                ProductId = entry.First.ProductId,
                quantityBeforeInvoice = entry.First.quantity + entry.Second.Quantity
            });

        var products = dbContext.Products
            .Where(product => invoiceProductIds.Any(productId => productId == product.Id))
            .ToList();

        var invoiceProductIdsWithMinLevelNotExceededBeforeInvoice = invoiceProductIdsAndQuantitiesBeforeInvoice
            .Zip(products)
            .Where(entry => entry.First.quantityBeforeInvoice >= entry.Second.MinLevel)
            .Select(entry => entry.First.ProductId);

        var invoiceProductsWithNewMinLevelWarnings = invoiceProductIdsAndQuantities
            .Where(entry => invoiceProductIdsWithMinLevelNotExceededBeforeInvoice.Contains(entry.ProductId))
            .Zip(products)
            .Where(entry => entry.Second.MinLevel > entry.First.quantity)
            .Select(entry => entry.Second);

        return invoiceProductsWithNewMinLevelWarnings
            .AsQueryable()
            .ProjectTo<Product>(Mapper.ConfigurationProvider);
    }

    public IQueryable<Product> GetAllWithNewMinLevelResolved(int invoiceId)
    {
        var invoiceMovements = dbContext.ProductMovements
            .Where(movement => movement.InvoiceId == invoiceId)
            .ToList();

        var invoiceProductIds = invoiceMovements
            .Select(movement => movement.ProductId.GetValueOrDefault())
            .ToList();

        var invoiceProductIdsAndQuantities = dbContext.ProductMovements
            .Where(movement => invoiceProductIds.Any(productId => productId == movement.ProductId.GetValueOrDefault()))
            .GroupBy(
                movement => movement.ProductId.GetValueOrDefault(),
                m => m.Type == ProductMovementType.In ? m.Quantity : -m.Quantity,
                (productId, quantities) => new {ProductId = productId, quantity = quantities.Sum()}
            )
            .ToList();

        var invoiceProductIdsAndQuantitiesBeforeInvoice = invoiceProductIdsAndQuantities
            .Zip(invoiceMovements)
            .Select(entry => new
            {
                ProductId = entry.First.ProductId,
                quantityBeforeInvoice = entry.First.quantity - entry.Second.Quantity
            });

        var products = dbContext.Products
            .Where(product => invoiceProductIds.Any(productId => productId == product.Id))
            .ToList();

        var invoiceProductIdsWithMinLevelExceededBeforeInvoice = invoiceProductIdsAndQuantitiesBeforeInvoice
            .Zip(products)
            .Where(entry => entry.First.quantityBeforeInvoice < entry.Second.MinLevel)
            .Select(entry => entry.First.ProductId);

        var invoiceProductsWithNewMinLevelResolves = invoiceProductIdsAndQuantities
            .Where(entry => invoiceProductIdsWithMinLevelExceededBeforeInvoice.Contains(entry.ProductId))
            .Zip(products)
            .Where(entry => entry.Second.MinLevel <= entry.First.quantity)
            .Select(entry => entry.Second);

        return invoiceProductsWithNewMinLevelResolves
            .AsQueryable()
            .ProjectTo<Product>(Mapper.ConfigurationProvider);
    }

    protected override IQueryable<ProductDb> GetIncludedDbSet()
    {
        return DbSet
            .Include(p => p.Category)
            .Include(p => p.Currency)
            .Include(p => p.Manufacturer)
            .Include(p => p.Unit);
    }
}