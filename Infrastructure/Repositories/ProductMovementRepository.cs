using System.Reflection;
using Application.Common.Dtos;
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

    public IQueryable<AggregateProductQuantity> AggregateProductsQuantities(ProductMovementFilters? filters = default)
    {
        var select = DbSet
            .Include(movement => movement.Product)
            .Include(movement => movement.Invoice)
            .Select(movement => new AggregateProductMovement
            {
                Id = movement.Id,
                ProductId = movement.Product!.Id,
                Product = movement.Product,
                Quantity = movement.Quantity,
                Type = movement.Type,
                AccountId = movement.Invoice.AccountId,
                WarehouseId = movement.Invoice.WarehouseId,
                CreatedAt = movement.CreatedAt,
                StoragePlaceId = movement.PlaceId,
            });

        if (filters != null)
        {
            select = new AggregateQuantitiesFilterHandler(filters).ApplyFilters(select);
        }

        var query = select
            .GroupBy(movement => new
            {
                movement.ProductId,
                ProductName = movement.Product!.Name,
                ProductCode = movement.Product.Barcode,
            })
            .Select(movementsGrouping => new AggregateProductQuantity
            {
                Product = new Product
                {
                    Id = (int) movementsGrouping.Key.ProductId!,
                    Name = movementsGrouping.Key.ProductName,
                    Barcode = movementsGrouping.Key.ProductCode,
                },
                InputQuantities = movementsGrouping.Where(movement => movement.Type == ProductMovementType.In)
                    .Sum(movement => movement.Quantity),
                OutputQuantities = movementsGrouping.Where(movement => movement.Type == ProductMovementType.Out)
                    .Sum(movement => movement.Quantity),
            });

        return query;
    }

    protected override IQueryable<ProductMovementDb> GetIncludedDbSet()
    {
        return DbSet.Include(item => item.CurrencyAmounts)!
            .ThenInclude(currencyAmount => currencyAmount.Currency)
            .Include(item => item.Product)
            .Include(item => item.Currency);
    }
}

internal class AggregateQuantitiesFilterHandler
{
    private readonly ProductMovementFilters _filters;

    public AggregateQuantitiesFilterHandler(ProductMovementFilters filters)
    {
        _filters = filters;
    }

    public IQueryable<AggregateProductMovement> ApplyFilters(IQueryable<AggregateProductMovement> query)
    {
        var filterMethods = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(m => m.Name.StartsWith("_filter"));
        
        return filterMethods.Aggregate(query,
            (current, filterMethod) =>
                (IQueryable<AggregateProductMovement>?) filterMethod.Invoke(this, new object[] {current})!);
    }

    private IQueryable<AggregateProductMovement> _filterProductIds(IQueryable<AggregateProductMovement> query)
    {
        return _filters.ProductIds is null
            ? query
            : query.Where(movement =>
                movement.ProductId != null && _filters.ProductIds!.Contains((int) movement.ProductId));
    }

    private IQueryable<AggregateProductMovement> _filterCategory(IQueryable<AggregateProductMovement> query)
    {
        return _filters.CategoryId is null
            ? query
            : query.Where(movement => movement.Product!.CategoryId == _filters.CategoryId);
    }

    private IQueryable<AggregateProductMovement> _filterAccount(IQueryable<AggregateProductMovement> query)
    {
        return _filters.AccountId is null
            ? query
            : query.Where(movement => movement.AccountId == _filters.AccountId);
    }

    private IQueryable<AggregateProductMovement> _filterStartDate(IQueryable<AggregateProductMovement> query)
    {
        return _filters.StartDate is null
            ? query
            : query.Where(movement => movement.CreatedAt >= _filters.StartDate);
    }

    private IQueryable<AggregateProductMovement> _filterEndDate(IQueryable<AggregateProductMovement> query)
    {
        return _filters.EndDate is null
            ? query
            : query.Where(movement => movement.CreatedAt <= _filters.EndDate);
    }

    private IQueryable<AggregateProductMovement> _filterWarehouse(IQueryable<AggregateProductMovement> query)
    {
        return _filters.WarehouseId is null
            ? query
            : query.Where(movement => movement.WarehouseId == _filters.WarehouseId);
    }

    private IQueryable<AggregateProductMovement> _filterManufacturer(IQueryable<AggregateProductMovement> query)
    {
        return _filters.ManufacturerId is null
            ? query
            : query.Where(movement => movement.Product!.ManufacturerId == _filters.ManufacturerId);
    }

    private IQueryable<AggregateProductMovement> _filterStoragePlace(IQueryable<AggregateProductMovement> query)
    {
        return _filters.PlaceId is null
            ? query
            : query.Where(movement => movement.StoragePlaceId == _filters.PlaceId);
    }
}

public record AggregateProductMovement
{
    public int Id { get; set; }
    public int? ProductId { get; set; }
    public ProductDb? Product { get; set; }
    public int? AccountId { get; set; }
    public int WarehouseId { get; set; }
    public int? StoragePlaceId { get; set; }
    public int Quantity { get; set; }
    public ProductMovementType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}