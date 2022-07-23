using Application.Common.Security;
using Microsoft.AspNetCore.Authorization;

namespace Authorization;

public class PoliciesAuthorizationRequirement : IAuthorizationRequirement
{
    public PoliciesAuthorizationRequirement(IEnumerable<Policy> policies)
    {
        Policies = policies;
    }
    
    public IEnumerable<Policy> Policies { get; }
}