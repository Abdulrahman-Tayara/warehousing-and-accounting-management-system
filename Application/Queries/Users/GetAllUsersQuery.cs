using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Users;

public class GetAllUsersQuery : GetPaginatedQuery<User>
{
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