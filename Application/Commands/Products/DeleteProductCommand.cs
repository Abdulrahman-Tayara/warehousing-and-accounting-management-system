using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Products;

[Authorize(Method = Method.Delete, Resource = Resource.Products)]
public class DeleteProductCommand : DeleteEntityCommand<int>
{
    
}

public class DeleteProductCommandHandler : DeleteEntityCommandHandler<DeleteProductCommand, Product, int, IProductRepository>
{
    public DeleteProductCommandHandler(IProductRepository repository) : base(repository)
    {
    }
}