using Application.Repositories;
using MediatR;
using Unit = Domain.Entities.Unit;

namespace Application.Commands.Units;

public class UpdateUnitCommand : IRequest<int>
{
    public int Id { get; set; }

    public string Name { get; init; }

    public int Value { get; set; }
}

public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, int>
{
    private readonly IUnitRepository _repository;

    public UpdateUnitCommandHandler(IUnitRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        var updatingEntity = new Unit {Id = request.Id, Name = request.Name, Value = request.Value};

        await _repository.Update(updatingEntity);

        await _repository.SaveChanges();

        return updatingEntity.Id;
    }
}