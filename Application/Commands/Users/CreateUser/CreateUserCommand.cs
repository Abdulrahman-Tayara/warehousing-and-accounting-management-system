using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Users.CreateUser;

public class CreateUserCommand : IRequest<int>
{
    public string Username { get; set; }
    
    public string Password { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            UserName = request.Username,
            PasswordHash = request.Password
        };

        var createdUser = await _userRepository.CreateAsync(user);

        return createdUser.Id;
    }
}