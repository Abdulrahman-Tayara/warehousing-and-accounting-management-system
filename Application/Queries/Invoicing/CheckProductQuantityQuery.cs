using Application.Queries.Invoicing.Dto;
using Application.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Queries.Invoicing;

public class CheckProductQuantityQuery : IRequest
{
    public IEnumerable<CheckProductQuantityDto> ProductQuantities;
}

public class CheckProductQuantityResult : RequestHandler<CheckProductQuantityQuery>
{
    private readonly IProductMovementRepository _productMovementRepository;

    public CheckProductQuantityResult(IProductMovementRepository productMovementRepository)
    {
        _productMovementRepository = productMovementRepository;
    }

    protected override void Handle(CheckProductQuantityQuery request)
    {
        IList<CheckProductQuantityDto> productQuantities = request.ProductQuantities
            .OrderBy(i => i.ProductId)
            .ToList();

        IEnumerable<int> productIdsExceedsMinLevel = _productMovementRepository
            .AggregateProductsQuantities(productQuantities.Select(dto => dto.ProductId).ToList())
            .OrderBy(dto => dto.Product.Id)
            .Zip(productQuantities)
            .Where(entry => _ExceedsProductMinLevel(entry.First, entry.Second))
            .Select(entry => entry.First.Product.Id)
            .ToList();

        if (productIdsExceedsMinLevel.Any())
        {
            throw new ProductMinLevelExceededException(productIdsExceedsMinLevel.ToList());
        }
    }

    private bool _ExceedsProductMinLevel(AggregateProductQuantity aggregate, CheckProductQuantityDto dto)
    {
        return aggregate.ExceedsMinLevel(dto.Quantity);
    }
}