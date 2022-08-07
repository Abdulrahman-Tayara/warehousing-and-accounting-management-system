using Application.Commands.Common;
using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Accounts;

[Authorize(Method = Method.Update, Resource = Resource.Accounts)]
public class UpdateAccountCommand : IUpdateEntityCommand<int>
{
    public int Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string City { get; set; }
}

public class
    UpdateAccountCommandHandler : UpdateEntityCommandHandler<UpdateAccountCommand, Account, int, IAccountRepository>
{
    public UpdateAccountCommandHandler(IAccountRepository repository) : base(repository)
    {
    }

    protected override Account GetEntityToUpdate(UpdateAccountCommand request) => new(
        id: request.Id,
        name: request.Name,
        code: request.Code,
        phone: request.Phone,
        city: request.City
    );
}