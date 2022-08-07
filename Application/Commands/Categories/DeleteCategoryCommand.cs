using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Categories;

[Authorize(Method = Method.Delete, Resource = Resource.Categories)]
public class DeleteCategoryCommand : DeleteEntityCommand<int>
{
    
}

public class DeleteCategoryCommandHandler : DeleteEntityCommandHandler<DeleteCategoryCommand, Category, int, ICategoryRepository>
{
    public DeleteCategoryCommandHandler(ICategoryRepository repository) : base(repository)
    {
    }
}