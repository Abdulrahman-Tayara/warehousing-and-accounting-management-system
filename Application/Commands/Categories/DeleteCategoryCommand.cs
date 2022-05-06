using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Categories;

public class DeleteCategoryCommand : DeleteEntityCommand<int>
{
    
}

public class DeleteCategoryCommandHandler : DeleteEntityCommandHandler<DeleteCategoryCommand, Category, int, ICategoryRepository>
{
    public DeleteCategoryCommandHandler(ICategoryRepository repository) : base(repository)
    {
    }
}