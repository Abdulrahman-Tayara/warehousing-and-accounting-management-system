using Application.Common.Security;
using Application.Repositories;
using MediatR;

namespace Application.Queries.Roles;

public class GetRoleQuery : IRequest<Role>
{
    public int Id { get; set; }
}

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, Role>
{
    private readonly IRoleRepository _roleRepository;

    public GetRoleQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public Task<Role> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        return _roleRepository.FindByIdAsync(request.Id);
    }
}