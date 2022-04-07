using Application.Repositories;
using MediatR;
using Unit = Domain.Entities.Unit;

namespace Application.Commands.Units;

public class CreateUnitCommand : IRequest<int>
{
    public string Name { get; init; }

    public int Value { get; set; }
}

public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, int>
{
    private readonly IUnitRepository _repository;

    public CreateUnitCommandHandler(IUnitRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = new Unit
        {
            Name = request.Name,
            Value = request.Value
        };

        var createdUnit = await _repository.CreateAsync(unit);

        await _repository.SaveChanges();

        return createdUnit.Id;
    }
}