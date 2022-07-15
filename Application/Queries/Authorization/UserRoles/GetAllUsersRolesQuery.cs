using Application.Common.Models;
using Application.Repositories.Aggregates;
using MediatR;

namespace Application.Queries.Authorization.UserRoles;

public class GetAllUsersRolesQuery : IRequest<IPaginatedEnumerable<Application.Common.Security.UserRoles>>
{
    
}

public class GetAllUsersRolesQueryHandler : IRequestHandler<GetAllUsersRolesQuery,
    IPaginatedEnumerable<Application.Common.Security.UserRoles>>
{
    private readonly IUserRolesRepository _userRolesRepository;

    public GetAllUsersRolesQueryHandler(IUserRolesRepository userRolesRepository)
    {
        _userRolesRepository = userRolesRepository;
    }

    public Task<IPaginatedEnumerable<Application.Common.Security.UserRoles>> Handle(GetAllUsersRolesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_userRolesRepository.GetAll().AsPaginatedQuery());
    }
}