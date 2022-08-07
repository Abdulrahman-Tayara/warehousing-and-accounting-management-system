using Application.Common.Security;
using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Products;

[Authorize(Method = Method.Read, Resource = Resource.Products)]
public class GetAllProductsInStoragePlaceQuery : GetPaginatedQuery<Product>
{
    public int Page { get; set; }
    
    public int PageSize { get; set; }
    
    public int StoragePlaceId { get; set; }

    public bool? IncludeStoragePlaceChildren { get; set; } = true;
}

public class GetAllProductsInStoragePlaceQueryHandler 
    : PaginatedQueryHandler<GetAllProductsInStoragePlaceQuery, Product>
{

    private readonly IProductRepository _productRepository;

    public GetAllProductsInStoragePlaceQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    protected override Task<IQueryable<Product>> GetQuery(GetAllProductsInStoragePlaceQuery request, CancellationToken cancellationToken)
    {
        var query = _productRepository.GetAllInStoragePlace(request.StoragePlaceId,
            request.IncludeStoragePlaceChildren.GetValueOrDefault());

        return Task.FromResult(query);
    }
}