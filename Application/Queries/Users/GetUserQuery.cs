using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Users;

public class GetUserQuery : IRequest<User>
{
    public int Id { get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return _userRepository.FindByIdAsync(request.Id);
    }
}