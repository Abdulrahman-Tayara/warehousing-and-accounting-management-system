using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Accounts;

public class CreateAccountCommand : ICreateEntityCommand<int>
{
    public string Code { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string City { get; set; }
}

public class
    CreateAccountCommandHandler : CreateEntityCommandHandler<CreateAccountCommand, Account, int, IAccountRepository>
{
    public CreateAccountCommandHandler(IAccountRepository repository) : base(repository)
    {
    }

    protected override Account CreateEntity(CreateAccountCommand request) => new(
        id: default,
        name: request.Name,
        code: request.Code,
        phone: request.Phone,
        city: request.City
    );
}