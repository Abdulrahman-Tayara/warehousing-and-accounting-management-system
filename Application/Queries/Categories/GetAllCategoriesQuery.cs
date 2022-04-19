using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Categories;

public class GetAllCategoriesQuery : GetPaginatedQuery<Category>
{
}

public class GetAllCategoriesQueryHandler : PaginatedQueryHandler<GetAllCategoriesQuery, Category>
{

    private readonly ICategoryRepository _repository;
    
    public GetAllCategoriesQueryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    protected override Task<IQueryable<Category>> GetQuery(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetAll());
    }
}