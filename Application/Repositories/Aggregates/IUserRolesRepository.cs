using Application.Common.Security;

namespace Application.Repositories.Aggregates;

public interface IUserRolesRepository
{
    Task<UserRoles> FindByUserId(int userId);
    
    Task<UserRoles> Update(UserRoles userRoles);
}