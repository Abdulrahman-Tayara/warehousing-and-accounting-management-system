using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Products;

public class CreateProductCommand : ICreateEntityCommand<int>
{
    public string Name { get; init; }

    public int CategoryId { get; init; }

    public int ManufacturerId { get; init; }

    public int UnitId { get; init; }

    public string Barcode { get; init; }

    public double Price { get; init; }

    public int CurrencyId { get; init; }

    public int? MinLevel { get; set; }
}

public class
    CreateProductCommandHandler : CreateEntityCommandHandler<CreateProductCommand, Product, int, IProductRepository>
{
    public CreateProductCommandHandler(IProductRepository repository) : base(repository)
    {
    }

    protected override Product CreateEntity(CreateProductCommand request)
    {
        return new Product
        {
            Name = request.Name,
            CategoryId = request.CategoryId,
            ManufacturerId = request.ManufacturerId,
            UnitId = request.UnitId,
            Barcode = request.Barcode,
            Price = request.Price,
            CurrencyId = request.CurrencyId,
            MinLevel = request.MinLevel ?? 0
        };
    }
}