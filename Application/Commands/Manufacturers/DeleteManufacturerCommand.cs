using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Manufacturers;

public class DeleteManufacturerCommand : DeleteEntityCommand<int>
{
    
}

public class DeleteManufacturerCommandHandler : DeleteEntityCommandHandler<DeleteManufacturerCommand, Manufacturer, int, IManufacturerRepository>
{
    public DeleteManufacturerCommandHandler(IManufacturerRepository repository) : base(repository)
    {
    }
}