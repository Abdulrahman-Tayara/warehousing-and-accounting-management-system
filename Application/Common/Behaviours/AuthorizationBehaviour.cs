using System.Reflection;
using Application.Common.Security;
using Application.Exceptions;
using Application.Services.Identity;
using MediatR;

namespace Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public AuthorizationBehaviour(ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var authorizeAttributes = request?.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes != null)
        {
            authorizeAttributes = authorizeAttributes.ToList();

            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (_currentUserService.UserId == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var authorizeAttributesWithPolicies = authorizeAttributes;
                authorizeAttributesWithPolicies = authorizeAttributesWithPolicies.ToList();
                if (authorizeAttributesWithPolicies.Any())
                {
                    var policies = authorizeAttributesWithPolicies.Select(a => new Policy(a.Resource, a.Method))
                        .Distinct()
                        .ToList();

                    var authorized = await _identityService.AuthorizeAsync((int) _currentUserService.UserId!, policies);

                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }
            }
        }

        return await next();
    }
}