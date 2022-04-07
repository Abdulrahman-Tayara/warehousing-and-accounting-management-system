using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Categories;

public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
{
}

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{

    private readonly ICategoryRepository _repository;
    
    public GetAllCategoriesQueryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetAll());
    }
}