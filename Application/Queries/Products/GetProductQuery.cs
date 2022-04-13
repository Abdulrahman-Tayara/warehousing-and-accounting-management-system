using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Products;

public class GetProductQuery : IRequest<Product>
{
    public int Id { get; set; }
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
{
    private readonly IProductRepository _repository;

    public GetProductQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.FindByIdAsync(request.Id, options: new FindOptions {IncludeRelations = true});

        return result;
    }
}