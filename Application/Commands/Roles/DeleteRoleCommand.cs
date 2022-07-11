using Application.Repositories;
using MediatR;

namespace Application.Commands.Roles;

public class DeleteRoleCommand : IRequest
{
    public int Id;
}

public class DeleteRoleCommandHandler : AsyncRequestHandler<DeleteRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public DeleteRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    protected override Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        return _roleRepository.DeleteAsync(request.Id);
    }
}