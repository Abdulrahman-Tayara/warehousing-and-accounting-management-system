using Application.Common.Security;
using Application.Queries.Common;
using Application.Repositories;

namespace Application.Queries.Authorization.Roles;

[Authorize(Method = Method.Read, Resource = Resource.Roles)]
public class GetAllRolesQuery : GetPaginatedQuery<Role>
{
}

public class GetAllRolesQueryHandler : PaginatedQueryHandler<GetAllRolesQuery, Role>
{
    private readonly IRoleRepository _roleRepository;

    public GetAllRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    protected override Task<IQueryable<Role>> GetQuery(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_roleRepository.GetAll());
    }
}