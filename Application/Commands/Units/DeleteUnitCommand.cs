using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Units;

[Authorize(Method = Method.Delete, Resource = Resource.Units)]
public class DeleteUnitCommand : DeleteEntityCommand<int>
{
    
}

public class DeleteUnitCommandHandler : DeleteEntityCommandHandler<DeleteUnitCommand, Unit, int, IUnitRepository>
{
    public DeleteUnitCommandHandler(IUnitRepository repository) : base(repository)
    {
    }
}