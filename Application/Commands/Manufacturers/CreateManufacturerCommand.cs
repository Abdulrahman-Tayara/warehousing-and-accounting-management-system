using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Manufacturers;

[Authorize(Method = Method.Write, Resource = Resource.Manufacturers)]
public class CreateManufacturerCommand : ICreateEntityCommand<int>
{
    public string Name { get; set; }

    public string? Code { get; set; }
}

public class CreateManufacturerCommandHandler : CreateEntityCommandHandler<CreateManufacturerCommand, Manufacturer, int,
    IManufacturerRepository>
{
    public CreateManufacturerCommandHandler(IManufacturerRepository repository) : base(repository)
    {
    }

    protected override Manufacturer CreateEntity(CreateManufacturerCommand request)
    {
        return new Manufacturer
        {
            Name = request.Name,
            Code = request.Code
        };
    }
}