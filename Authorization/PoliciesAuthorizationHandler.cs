using Application.Common.Security;
using Microsoft.AspNetCore.Authorization;

namespace Authorization;

public class PoliciesAuthorizationHandler : AuthorizationHandler<PoliciesAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PoliciesAuthorizationRequirement requirement)
    {
        var userPermissions = context.User.FindFirst(
            c => c.Type == AuthorizationClaimTypes.Permissions);

        if (userPermissions == null)
        {
            return Task.CompletedTask;
        }
        
        var requiredPermissions = requirement.Policies;

        var authorized = PolicyAuthorizer.Authorize(userPermissions.Value, requiredPermissions);

        if (!authorized) return Task.CompletedTask;

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}