using Application.Commands.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Commands.Users.CreateUser;

public class CreateUserCommand : ICreateEntityCommand<int>
{
    public string Username { get; set; }
    
    public string Password { get; set; }
}

public class CreateUserCommandHandler : CreateEntityCommandHandler<CreateUserCommand, User, int, IUserRepository>
{
    public CreateUserCommandHandler(IUserRepository repository) : base(repository)
    {
    }

    protected override User CreateEntity(CreateUserCommand request)
    {
        return new User
        {
            UserName = request.Username,
            PasswordHash = request.Password
        };
    }
}