using Application.Commands.Common;
using Application.Common.QueryFilters;
using Application.Common.Security;
using Application.Repositories;
using Application.Repositories.UnitOfWork;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Users.CreateUser;

[Authorize(Method = Method.Write, Resource = Resource.Users)]
public class CreateUserCommand : ICreateEntityCommand<int>
{
    public string Username { get; set; }

    public string Password { get; set; }
    
    [QueryFilter(QueryFilterCompareType.InArray, FieldName = "Id")]
    public IEnumerable<int> RoleIds { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly Lazy<IUnitOfWork> _unitOfWork;
    private IUnitOfWork? _openedUnitOfWork;
    
    public CreateUserCommandHandler(Lazy<IUnitOfWork> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        using var unitOfWork = _unitOfWork.Value;
        _openedUnitOfWork = unitOfWork;

        var user = await CreateUser(request);

        await AssignRoles(request, user);

        return user.Id;
    }

    private async Task<User> CreateUser(CreateUserCommand request)
    {
        var user = CreateEntity(request);
        
        var saveAction = await _openedUnitOfWork!.UserRepository.CreateAsync(user);

        var createdUser = await saveAction();

        return createdUser;
    }

    private async Task AssignRoles(CreateUserCommand request, User user)
    {
        var userRoles = await _openedUnitOfWork!.UserRolesRepository.FindByUserId(user.Id);
        var roles = GetRoles(request);
        userRoles.UpdateRoles(roles);
        await _openedUnitOfWork.UserRolesRepository.Update(userRoles);
    }

    private IList<Role> GetRoles(CreateUserCommand request)
    {
        var roles = _openedUnitOfWork!.RoleRepository.GetAll().WhereFilters(request);
        return roles.ToList();
    }

    private static User CreateEntity(CreateUserCommand request)
    {
        return new User
        {
            UserName = request.Username,
            PasswordHash = request.Password
        };
    }
}