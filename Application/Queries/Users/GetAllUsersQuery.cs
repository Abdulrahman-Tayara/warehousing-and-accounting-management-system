using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Users;

public class GetAllUsersQuery : IRequest<IEnumerable<User>>
{
    
}

public class GetAllUsersQueryHandler : RequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override IEnumerable<User> Handle(GetAllUsersQuery request)
    {
        return _userRepository.GetAll();
    }
}