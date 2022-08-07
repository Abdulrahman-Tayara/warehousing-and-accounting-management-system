using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.CountryOrigins;

[Authorize(Method = Method.Update, Resource = Resource.Countries)]
public partial class UpdateCountryOriginCommand : IUpdateEntityCommand<int>
{
    public int Id { get; set; }

    public string Name { get; init; }
}

public class UpdateCountryOriginCommandHandler : UpdateEntityCommandHandler<UpdateCountryOriginCommand, CountryOrigin, int, ICountryOriginRepository>
{
    public UpdateCountryOriginCommandHandler(ICountryOriginRepository repository) : base(repository)
    {
    }

    protected override CountryOrigin GetEntityToUpdate(UpdateCountryOriginCommand request)
    {
        return new CountryOrigin {Id = request.Id, Name = request.Name};
    }
}