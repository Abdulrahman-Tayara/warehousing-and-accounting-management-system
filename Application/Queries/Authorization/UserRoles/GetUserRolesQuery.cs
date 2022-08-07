using Application.Common.Models;
using Application.Common.Security;
using Application.Queries.Common;
using Application.Repositories.Aggregates;
using MediatR;

namespace Application.Queries.Authorization.UserRoles;

public class GetUserRolesQuery : IRequest<IPaginatedEnumerable<Role>>
{
    public int UserId { get; set; }
}

public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, IPaginatedEnumerable<Role>>
{
    private readonly IUserRolesRepository _userRolesRepository;

    public GetUserRolesQueryHandler(IUserRolesRepository userRolesRepository)
    {
        _userRolesRepository = userRolesRepository;
    }

    public async Task<IPaginatedEnumerable<Role>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        var userRoles = await _userRolesRepository.FindByUserId(request.UserId);
        
        return new EnumerableQuery<Role>(userRoles.Roles).AsPaginatedQuery();
    }
}
