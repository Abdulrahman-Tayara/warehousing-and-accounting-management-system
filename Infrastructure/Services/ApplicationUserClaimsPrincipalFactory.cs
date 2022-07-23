using System.Security.Claims;
using Application.Common.Security;
using Authorization;
using Infrastructure.Persistence.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationIdentityUser, ApplicationRole>
{
    public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationIdentityUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationIdentityUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        
        var userRoleNames = await UserManager.GetRolesAsync(user);

        var userRoles = await RoleManager.Roles.Where(r =>
            userRoleNames.Contains(r.Name)).ToListAsync();

        var userPermissions = new Permissions();
        
        foreach (var role in userRoles)
        {
            userPermissions.Merge(Permissions.From(role.Permissions));
        }

        var permissionsString = userPermissions.ToString();
        
        identity.AddClaim(
            new Claim(AuthorizationClaimTypes.Permissions, permissionsString));
        
        return identity;
    }
}