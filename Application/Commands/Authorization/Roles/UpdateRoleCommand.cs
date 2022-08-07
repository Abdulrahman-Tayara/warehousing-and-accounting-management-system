using Application.Common.Security;
using Application.Repositories;
using MediatR;

namespace Application.Commands.Authorization.Roles;

[Authorize(Method = Method.Update, Resource = Resource.Roles)]
public class UpdateRoleCommand : IRequest<int>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Permissions Permissions { get; set; }
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, int>
{
    private readonly IRoleRepository _roleRepository;

    public UpdateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<int> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.Update(new Role(
            request.Id, request.Name, request.Permissions));

        return role.Id;
    }
}