using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Categories;

[Authorize(Method = Method.Write, Resource = Resource.Categories)]
public class CreateCategoryCommand : ICreateEntityCommand<int>
{
    public string Name { get; init; }
}

public class CreateCategoryCommandHandler : CreateEntityCommandHandler<CreateCategoryCommand, Category, int, ICategoryRepository>
{
    public CreateCategoryCommandHandler(ICategoryRepository repository) : base(repository)
    {
    }

    protected override Category CreateEntity(CreateCategoryCommand request)
    {
        return new Category
        {
            Name = request.Name
        };
    }
}