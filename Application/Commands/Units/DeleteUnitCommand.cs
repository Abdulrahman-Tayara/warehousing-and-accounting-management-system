using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Units;

public class DeleteUnitCommand : DeleteEntityCommand<int>
{
    
}

public class DeleteUnitCommandHandler : DeleteEntityCommandHandler<DeleteUnitCommand, Unit, int, IUnitRepository>
{
    public DeleteUnitCommandHandler(IUnitRepository repository) : base(repository)
    {
    }
}