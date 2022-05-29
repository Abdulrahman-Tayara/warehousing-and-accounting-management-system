using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.CountryOrigins;

public class DeleteCountryOriginCommand : DeleteEntityCommand<int>
{
    
}

public class DeleteCountryOriginCommandHandler : DeleteEntityCommandHandler<DeleteCountryOriginCommand, CountryOrigin, int, ICountryOriginRepository>
{
    public DeleteCountryOriginCommandHandler(ICountryOriginRepository repository) : base(repository)
    {
    }
}