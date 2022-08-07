using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.CountryOrigins;

[Authorize(Method = Method.Delete, Resource = Resource.Countries)]
public class DeleteCountryOriginCommand : DeleteEntityCommand<int>
{
    
}

public class DeleteCountryOriginCommandHandler : DeleteEntityCommandHandler<DeleteCountryOriginCommand, CountryOrigin, int, ICountryOriginRepository>
{
    public DeleteCountryOriginCommandHandler(ICountryOriginRepository repository) : base(repository)
    {
    }
}