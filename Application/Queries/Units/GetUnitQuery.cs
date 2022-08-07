using Application.Common.Security;
using Application.Repositories;
using MediatR;
using Unit = Domain.Entities.Unit;

namespace Application.Queries.Units;

[Authorize(Method = Method.Read, Resource = Resource.Units)]
public class GetUnitQuery : IRequest<Unit>
{
    public int Id { get; init; }
}

public class GetUnitQueryHandler : IRequestHandler<GetUnitQuery, Unit>
{
    private readonly IUnitRepository _repository;

    public GetUnitQueryHandler(IUnitRepository repository)
    {
        _repository = repository;
    }

    public Task<Unit> Handle(GetUnitQuery request, CancellationToken cancellationToken)
    {
        return _repository.FindByIdAsync(request.Id);
    }
}