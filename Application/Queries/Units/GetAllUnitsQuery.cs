using Application.Repositories;
using MediatR;
using Unit = Domain.Entities.Unit;

namespace Application.Queries.Units;

public class GetAllUnitsQuery : IRequest<IEnumerable<Unit>>
{
}

public class GetAllUnitsQueryHandler : IRequestHandler<GetAllUnitsQuery, IEnumerable<Unit>>
{

    private readonly IUnitRepository _repository;
    
    public GetAllUnitsQueryHandler(IUnitRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Unit>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetAll());
    }
}