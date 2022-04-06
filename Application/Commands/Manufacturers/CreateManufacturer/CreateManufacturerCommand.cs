using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Manufacturers.CreateManufacturer;

public class CreateManufacturerCommand : IRequest<int>
{
    public string Name { get; set; }
    
    public string? Code { get; set; }
}

public class CreateManufacturerCommandHandler : IRequestHandler<CreateManufacturerCommand, int>
{
    private readonly IManufacturerRepository _manufacturerRepository;

    public CreateManufacturerCommandHandler(IManufacturerRepository manufacturerRepository)
    {
        _manufacturerRepository = manufacturerRepository;
    }

    public async Task<int> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = new Manufacturer
        {
            Name = request.Name,
            Code = request.Code
        };

        manufacturer = await _manufacturerRepository.CreateAsync(manufacturer);

        await _manufacturerRepository.SaveChanges();

        return manufacturer.Id;
    }
}