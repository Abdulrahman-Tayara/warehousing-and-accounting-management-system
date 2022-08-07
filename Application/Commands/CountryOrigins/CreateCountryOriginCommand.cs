using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.CountryOrigins;

[Authorize(Method = Method.Write, Resource = Resource.Countries)]
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