using Application.Common.QueryFilters;
using Application.Common.Security;
using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Users;

[Authorize(Method = Method.Read, Resource = Resource.Users)]
public class GetAllUsersQuery : GetPaginatedQuery<User>
{
    [QueryFilter(QueryFilterCompareType.StringContains)]
    public string? UserName {get; set; }
}

public class GetAllUsersQueryHandler : PaginatedQueryHandler<GetAllUsersQuery, User>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override Task<IQueryable<User>> GetQuery(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_userRepository.GetAll());
    }
}