using Application.Common.Security;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Users.UpdateUser;

[Authorize(Method = Method.Update, Resource = Resource.Users)]
public class UpdateUserCommand : IRequest
{
    public int Id { get; set; }
    
    public string UserName { get; init; }
}

public class UpdateUserCommandHandler : AsyncRequestHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByIdAsync(request.Id);
        
        user.Update(new User
        {
            UserName = request.UserName
        });

        await _userRepository.Update(user);

        await _userRepository.SaveChanges();
    }
}