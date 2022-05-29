using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Categories;

public partial class UpdateCategoryCommand : IUpdateEntityCommand<int>
{
    public int Id { get; set; }

    public string Name { get; init; }
}

public class UpdateCategoryCommandHandler : UpdateEntityCommandHandler<UpdateCategoryCommand, Category, int, ICategoryRepository>
{
    public UpdateCategoryCommandHandler(ICategoryRepository repository) : base(repository)
    {
    }

    protected override Category GetEntityToUpdate(UpdateCategoryCommand request)
    {
        return new Category {Id = request.Id, Name = request.Name};
    }
}