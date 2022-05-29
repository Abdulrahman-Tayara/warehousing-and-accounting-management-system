using Application.Common.Dtos;
using Application.Queries.Invoicing.Dto;
using Application.Repositories;
using Domain.Aggregations;
using Domain.Exceptions;
using MediatR;

namespace Application.Queries.Invoicing;

public class CheckProductQuantityQuery : IRequest
{
    public IEnumerable<CheckProductQuantityDto> ProductQuantities { get; init; } = null!;

    public bool IgnoreMinLevelWarnings { get; set; } = true;
}

/// <summary>
/// <exception cref="ProductMinLevelExceededException"></exception>
/// <exception cref="ZeroLevelExceededException"></exception>
/// </summary>
public class CheckProductQuantityQueryHandler : RequestHandler<CheckProductQuantityQuery>
{
    private readonly IProductMovementRepository _productMovementRepository;

    public CheckProductQuantityQueryHandler(IProductMovementRepository productMovementRepository)
    {
        _productMovementRepository = productMovementRepository;
    }

    public void HandleQuery(CheckProductQuantityQuery query)
    {
        Handle(query);
    }

    protected override void Handle(CheckProductQuantityQuery request)
    {
        IList<CheckProductQuantityDto> productQuantities = request.ProductQuantities
            .OrderBy(i => i.ProductId)
            .ToList();

        var productIds = productQuantities.Select(dto => dto.ProductId).ToList();

        var aggregatesAndProductQuantities = _productMovementRepository
            .AggregateProductsQuantities(new ProductMovementFilters
            {
                ProductIds = productIds
            })
            .ToList()
            .OrderBy(dto => dto.Product!.Id)
            .Zip(productQuantities)
            .ToList();

        IEnumerable<int> productIdsExceedsZeroLevel = aggregatesAndProductQuantities
            .Where(entry => _exceedsZeroLevel(entry.First, entry.Second))
            .Select(entry => entry.First.Product!.Id)
            .ToList();

        if (productIdsExceedsZeroLevel.Any())
        {
            throw new ZeroLevelExceededException(productIdsExceedsZeroLevel.ToList());
        }

        if (request.IgnoreMinLevelWarnings)
            return;

        IEnumerable<int> productIdsExceedsMinLevel = aggregatesAndProductQuantities
            .Where(entry => _exceedsProductMinLevel(entry.First, entry.Second))
            .Select(entry => entry.First.Product!.Id)
            .ToList();

        if (productIdsExceedsMinLevel.Any())
        {
            throw new ProductMinLevelExceededException(productIdsExceedsMinLevel.ToList());
        }
    }

    private bool _exceedsProductMinLevel(AggregateProductQuantity aggregate, CheckProductQuantityDto dto)
    {
        return aggregate.ExceedsMinLevel(dto.Quantity);
    }

    private bool _exceedsZeroLevel(AggregateProductQuantity aggregate, CheckProductQuantityDto dto)
    {
        return aggregate.ExceedsZeroLevel(dto.Quantity);
    }
}