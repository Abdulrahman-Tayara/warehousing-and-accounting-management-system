using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Manufacturers;

[Authorize(Method = Method.Read, Resource = Resource.Manufacturers)]
public class GetManufacturerQuery : IRequest<Manufacturer>
{
    public int Id { get; set; }
}

public class GetManufacturerQueryHandler : IRequestHandler<GetManufacturerQuery, Manufacturer>
{

    private readonly IManufacturerRepository _manufacturerRepository;

    public GetManufacturerQueryHandler(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;
    }

    public Task<Manufacturer> Handle(GetManufacturerQuery request, CancellationToken cancellationToken)
    {
        return _manufacturerRepository.FindByIdAsync(request.Id);
    }
}