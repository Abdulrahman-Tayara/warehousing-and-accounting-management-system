using Application.Common.Dtos;
using Application.Common.QueryFilters;
using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

    public IQueryable<AggregateProductQuantity> AggregateProductsQuantities(ProductMovementFilters? filters = default)
    {
        var select = DbSet
            .Include(movement => movement.Invoice)
            .Select(movement => new AggregateProductMovement
            {
                Id = movement.Id,
                ProductId = movement.Product!.Id,
                CategoryId = movement.Product.CategoryId,
                ManufacturerId = movement.Product.ManufacturerId,
                Quantity = movement.Quantity,
                Type = movement.Type,
                AccountId = movement.Invoice.AccountId,
                WarehouseId = movement.Invoice.WarehouseId,
                CreatedAt = movement.CreatedAt,
                StoragePlaceId = movement.PlaceId,
            });

        if (filters != null)
        {
            select = select.WhereFilters(filters);
        }

        var aggregatesQuery = select
            .GroupBy(movement => movement.ProductId)
            .Select(movementsGrouping => new AggregateProductQuantity
            {
                ProductId = movementsGrouping.Key,
                InputQuantities = movementsGrouping.Where(movement => movement.Type == ProductMovementType.In)
                    .Sum(movement => movement.Quantity),
                OutputQuantities = movementsGrouping.Where(movement => movement.Type == ProductMovementType.Out)
                    .Sum(movement => movement.Quantity),
            });

        var aggregates = aggregatesQuery.ToList();

        var productIds = aggregates
            .Select(aggregate => aggregate.ProductId);

        var products = dbContext.Products.AsQueryable()
            .Include(p => p.Category)
            .Include(p => p.Currency)
            .Include(p => p.Manufacturer)
            .Include(p => p.Unit)
            .Where(product => productIds.Any(id => product.Id == id))
            .ProjectTo<Product>(Mapper.ConfigurationProvider)
            .ToList();

        var aggregatesWithFullProduct = aggregates
            .Zip(products)
            .Select(entry => entry.First.AddProduct(entry.Second));

        return aggregatesWithFullProduct.AsQueryable();
    }

    public IQueryable<AggregateStoragePlaceQuantity> AggregateStoragePlacesQuantities(
        ProductMovementFilters? filters = default)
    {
        var selected = DbSet
            .Include(movement => movement.Invoice)
            .Include(movement => movement.Product)
            .Select(movement => new
            {
                ProductId = movement.ProductId.GetValueOrDefault(),
                movement.Product!.CategoryId,
                movement.Product!.ManufacturerId,
                movement.Product!.CountryOriginId,
                movement.Quantity,
                AccountId = movement.Invoice.AccountId.GetValueOrDefault(),
                movement.Invoice.WarehouseId,
                StoragePlaceId = movement.PlaceId.GetValueOrDefault(),
                movement.Type,
                movement.CreatedAt
            });

        if (filters != null)
        {
            selected = selected.WhereFilters(filters);
        }

        var groupByResult =
            from m in selected
            group m by new {m.StoragePlaceId, m.ProductId}
            into g
            select new
            {
                g.Key.ProductId,
                Quantity = g.Sum(movement =>
                    movement.Type == ProductMovementType.In ? movement.Quantity : -movement.Quantity),
                g.Key.StoragePlaceId
            };

        var includedProducts = dbContext.Products
            .Include(p => p.Manufacturer)
            .Include(p => p.Unit)
            .Include(p => p.CountryOrigin);

        var includedStoragePlaces = dbContext.StoragePlaces
            .Include(sp => sp.Warehouse);

        var joined =
            from obj in groupByResult
            join product in includedProducts on obj.ProductId equals product.Id
            join storagePlace in includedStoragePlaces on obj.StoragePlaceId equals storagePlace.Id
            select new AggregateStoragePlaceQuantity(
                Mapper.Map<Product>(product),
                obj.Quantity,
                Mapper.Map<StoragePlace>(storagePlace)
            );

        return joined;
    }

    protected override IQueryable<ProductMovementDb> GetIncludedDbSet()
    {
        return DbSet.Include(item => item.CurrencyAmounts)!
            .ThenInclude(currencyAmount => currencyAmount.Currency)
            .Include(item => item.Product)
            .Include(item => item.Currency);
    }
}

public record AggregateProductMovement
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public ProductDb? Product { get; set; }
    public int? CategoryId { get; set; }
    public int? ManufacturerId { get; set; }
    public int? AccountId { get; set; }
    public int? WarehouseId { get; set; }
    public int? StoragePlaceId { get; set; }
    public int Quantity { get; set; }
    public ProductMovementType Type { get; set; }
    public DateTime? CreatedAt { get; set; }
}