using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
    {
        return services.AddSingleton<IAuthorizationHandler, PoliciesAuthorizationHandler>()
            .AddSingleton<IAuthorizationPolicyProvider, ApplicationAuthorizationPolicyProvider>();
    }
}