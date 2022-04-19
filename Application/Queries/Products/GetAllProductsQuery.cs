using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Products;

public class GetAllProductsQuery : GetPaginatedQuery<Product>
{
}

public class GetAllProductsQueryHandler : PaginatedQueryHandler<GetAllProductsQuery, Product>
{
    private readonly IProductRepository _repository;

    public GetAllProductsQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    protected override Task<IQueryable<Product>> GetQuery(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _repository.GetAll(new GetAllOptions<Product> {IncludeRelations = true}));
    }
}