using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Units;

[Authorize(Method = Method.Update, Resource = Resource.Units)]
public class UpdateUnitCommand : IUpdateEntityCommand<int>
{
    public int Id { get; set; }

    public string Name { get; init; }

    public int Value { get; set; }
}

public class UpdateUnitCommandHandler : UpdateEntityCommandHandler<UpdateUnitCommand, Unit, int, IUnitRepository>
{
    public UpdateUnitCommandHandler(IUnitRepository repository) : base(repository)
    {
    }

    protected override Unit GetEntityToUpdate(UpdateUnitCommand request)
    {
        return new Unit {Id = request.Id, Name = request.Name, Value = request.Value};
    }
}