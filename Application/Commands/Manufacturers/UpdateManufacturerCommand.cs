using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Manufacturers;

[Authorize(Method = Method.Update, Resource = Resource.Manufacturers)]
public class UpdateManufacturerCommand : IUpdateEntityCommand<int>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Code { get; set; }
}

public class UpdateManufacturerCommandHandler : UpdateEntityCommandHandler<UpdateManufacturerCommand, Manufacturer, int,
    IManufacturerRepository>
{
    public UpdateManufacturerCommandHandler(IManufacturerRepository repository) : base(repository)
    {
    }

    protected override Manufacturer GetEntityToUpdate(UpdateManufacturerCommand request)
    {
        return new Manufacturer
        {
            Id = request.Id,
            Code = request.Code,
            Name = request.Name
        };
    }
}