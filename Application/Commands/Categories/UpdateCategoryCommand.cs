using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Categories;

public class UpdateCategoryCommand : IRequest<int>
{
    public int Id { get; set; }

    public string Name { get; init; }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, int>
{
    private readonly ICategoryRepository _repository;

    public UpdateCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var updatingEntity = new Category {Id = request.Id, Name = request.Name};

        await _repository.Update(updatingEntity);

        await _repository.SaveChanges();

        return updatingEntity.Id;
    }
}