using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Manufacturers;

public class GetAllManufacturersQuery : IRequest<IEnumerable<Manufacturer>>
{
    
}

public class GetAllManufacturersQueryHandler : RequestHandler<GetAllManufacturersQuery, IEnumerable<Manufacturer>>
{
    private readonly IManufacturerRepository _manufacturerRepository;

    public GetAllManufacturersQueryHandler(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;
    }

    protected override IEnumerable<Manufacturer> Handle(GetAllManufacturersQuery request)
    {
        return _manufacturerRepository.GetAll();
    }
}