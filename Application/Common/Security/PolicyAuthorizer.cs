namespace Application.Common.Security;

public static class PolicyAuthorizer
{
    public static bool Authorize(string policy, IEnumerable<Policy> requiredPolicies)
    {
        if (policy.Length == 0)
            return false;
        
        var permissions = Permissions.From(policy);

        if (permissions.AllPermissions)
            return true;
        if (permissions.None)
            return false;
        
        var authorized = requiredPolicies.Any(permissions.Policies.Contains);

        return authorized;
    }
}