using Application.Common.QueryFilters;
using Application.Common.Security;
using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Manufacturers;

[Authorize(Method = Method.Read, Resource = Resource.Manufacturers)]
public class GetAllManufacturersQuery : GetPaginatedQuery<Manufacturer>
{
    [QueryFilter(QueryFilterCompareType.StringContains)]
    public string? Name {get; set; }
}

public class GetAllManufacturersQueryHandler : PaginatedQueryHandler<GetAllManufacturersQuery, Manufacturer>
{
    private readonly IManufacturerRepository _manufacturerRepository;

    public GetAllManufacturersQueryHandler(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;
    }

    protected override Task<IQueryable<Manufacturer>> GetQuery(GetAllManufacturersQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_manufacturerRepository.GetAll());
    }

}