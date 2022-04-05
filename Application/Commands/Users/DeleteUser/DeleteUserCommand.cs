using Application.Repositories;
using MediatR;

namespace Application.Commands.Users.DeleteUser;

public class DeleteUserCommand : IRequest
{
    public int Id { get; init; }
}

public class DeleteUserCommandHandler : AsyncRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteAsync(request.Id);

        await _userRepository.SaveChanges();
    }
}