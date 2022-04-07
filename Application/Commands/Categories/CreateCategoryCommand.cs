using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Categories;

public class CreateCategoryCommand : IRequest<int>
{
    public string Name { get; init; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly ICategoryRepository _repository;

    public CreateCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name
        };

        var createdCategory = await _repository.CreateAsync(category);

        await _repository.SaveChanges();

        return createdCategory.Id;
    }
}