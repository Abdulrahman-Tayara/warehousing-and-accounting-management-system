using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Accounts;

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

    protected override Account GetEntityToUpdate(UpdateAccountCommand request) => new()
    {
        Id = request.Id,
        Name = request.Name,
        Code = request.Code,
        Phone = request.Phone,
        City = request.City
    };
}