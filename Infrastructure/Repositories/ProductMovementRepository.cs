using Application.Common.Dtos;
using Application.Common.QueryFilters;
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