using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Units;

[Authorize(Method = Method.Write, Resource = Resource.Units)]
public class CreateUnitCommand : ICreateEntityCommand<int>
{
    public string Name { get; init; }

    public int Value { get; set; }
}

public class CreateUnitCommandHandler : CreateEntityCommandHandler<CreateUnitCommand, Unit, int, IUnitRepository>
{
    public CreateUnitCommandHandler(IUnitRepository repository) : base(repository)
    {
    }

    protected override Unit CreateEntity(CreateUnitCommand request)
    {
        return new Unit
        {
            Name = request.Name,
            Value = request.Value
        };
    }
}