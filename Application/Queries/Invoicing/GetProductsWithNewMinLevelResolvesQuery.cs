using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Invoicing;

public class GetProductsWithNewMinLevelResolvesQuery : IRequest<IEnumerable<Product>>
{
    public int InvoiceId { get; }

    public GetProductsWithNewMinLevelResolvesQuery(int invoiceId)
    {
        InvoiceId = invoiceId;
    }
}

public class GetProductsWithMinLevelResolvesQueryHandler
    : IRequestHandler<GetProductsWithNewMinLevelResolvesQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsWithMinLevelResolvesQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<IEnumerable<Product>> Handle(
        GetProductsWithNewMinLevelResolvesQuery request,
        CancellationToken cancellationToken
    )
    {
        return Task.FromResult(_productRepository.GetAllWithNewMinLevelResolved(request.InvoiceId).AsEnumerable());
    }
}