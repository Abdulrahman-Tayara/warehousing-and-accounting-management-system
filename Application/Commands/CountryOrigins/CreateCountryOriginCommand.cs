using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.CountryOrigins;

public class CreateCountryOriginCommand : ICreateEntityCommand<int>
{
    public string Name { get; init; }
}

public class CreateCountryOriginCommandHandler : CreateEntityCommandHandler<CreateCountryOriginCommand, CountryOrigin, int, ICountryOriginRepository>
{
    public CreateCountryOriginCommandHandler(ICountryOriginRepository repository) : base(repository)
    {
    }

    protected override CountryOrigin CreateEntity(CreateCountryOriginCommand request)
    {
        return new CountryOrigin
        {
            Name = request.Name
        };
    }
}