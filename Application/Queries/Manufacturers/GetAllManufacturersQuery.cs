using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Manufacturers;

public class GetAllManufacturersQuery : GetPaginatedQuery<Manufacturer>
{
    
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