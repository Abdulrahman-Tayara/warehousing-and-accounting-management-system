using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Products;

[Authorize(Method = Method.Update, Resource = Resource.Products)]
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

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await _productRepository.FindByIdAsync(request.Id);

        product.Name = request.Name;
        product.CategoryId = request.CategoryId;
        product.ManufacturerId = request.ManufacturerId;
        product.CountryOriginId = request.CountryOriginId;
        product.UnitId = request.UnitId;
        product.Barcode = request.Barcode;
        product.Price = request.Price;
        product.CurrencyId = request.CurrencyId;
        product.UpdateMinLevel(request.MinLevel ?? 0);

        Product updatedProduct = await _productRepository.Update(product);

        await _productRepository.SaveChanges();

        return updatedProduct.Id;
    }
}