using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Products;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
{
}

public class GetAllProductsQueryHandler : RequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _repository;

    public GetAllProductsQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    protected override IEnumerable<Product> Handle(GetAllProductsQuery request)
    {
        return _repository.GetAllIncluded();
    }
}