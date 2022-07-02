using Application.Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Authorization;

public class ApplicationAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    private readonly AuthorizationOptions _options;
    
    public ApplicationAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
        _options = options.Value;
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        if (policy != null || !PolicyHelper.IsValidPolicy(policyName)) return policy;
        
        var policies = PolicyHelper.CreatePoliciesFromString(policyName);

        policy = new AuthorizationPolicyBuilder()
            .AddRequirements(new PoliciesAuthorizationRequirement(policies))
            .Build();
            
        _options.AddPolicy(policyName, policy);

        return policy;
    }
}