using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Products;

public class UpdateProductCommand : IUpdateEntityCommand<int>
{
    public int Id { get; set; }

    public string Name { get; init; }

    public int CategoryId { get; init; }

    public int ManufacturerId { get; init; }

    public int CountryOriginId { get; init; }

    public int UnitId { get; init; }

    public string Barcode { get; init; }

    public double Price { get; init; }

    public int CurrencyId { get; init; }

    public int? MinLevel { get; init; }
}

public class UpdateProductCommandHandler
    : UpdateEntityCommandHandler<UpdateProductCommand, Product, int, IProductRepository>
{
    public UpdateProductCommandHandler(IProductRepository repository) : base(repository)
    {
    }

    protected override Product GetEntityToUpdate(UpdateProductCommand request)
    {
        return new Product(
            id: request.Id,
            name: request.Name,
            categoryId: request.CategoryId,
            manufacturerId: request.ManufacturerId,
            countryOriginId: request.CountryOriginId,
            unitId: request.UnitId,
            barcode: request.Barcode,
            price: request.Price,
            currencyId: request.CurrencyId,
            minLevel: request.MinLevel ?? 0
        );
    }
}