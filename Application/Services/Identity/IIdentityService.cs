using Application.Common.Dtos;
using Application.Common.Security;
using Application.Exceptions;
using Domain.Entities;

namespace Application.Services.Identity;

public interface IIdentityService
{
    Task<bool> CheckPasswordAsync(User user, string password);

    /// <exception cref="NotFoundException"></exception>
    /// <param name="username"></param>
    /// <returns>The matched user</returns>
    Task<User> FindUserByNameAsync(string username);
    
    Task<bool> AuthorizeAsync(int userId, string policyName);
    Task<bool> AuthorizeAsync(int userId, IList<Policy> policies);
}