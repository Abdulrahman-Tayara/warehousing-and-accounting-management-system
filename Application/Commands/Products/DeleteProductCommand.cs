using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Products;

public class DeleteProductCommand : DeleteEntityCommand<int>
{
    
}

public class DeleteProductCommandHandler : DeleteEntityCommandHandler<DeleteProductCommand, Product, int, IProductRepository>
{
    public DeleteProductCommandHandler(IProductRepository repository) : base(repository)
    {
    }
}