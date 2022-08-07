using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Manufacturers;

[Authorize(Method = Method.Delete, Resource = Resource.Manufacturers)]
public class DeleteManufacturerCommand : DeleteEntityCommand<int>
{
    
}

public class DeleteManufacturerCommandHandler : DeleteEntityCommandHandler<DeleteManufacturerCommand, Manufacturer, int, IManufacturerRepository>
{
    public DeleteManufacturerCommandHandler(IManufacturerRepository repository) : base(repository)
    {
    }
}