using Application.Common.Security;
using FluentAssertions;
using NUnit.Framework;

namespace Application.UnitTests.Common.Security;

public class PolicyAuthorizerTests
{
    [Test]
    public void AuthorizedPermissions()
    {
        var permissions = new[]
        {
            "all",
            "All",
            "users-read"
        };

        foreach (var permission in permissions)
        {
            var authorized = PolicyAuthorizer.Authorize(permission, new[] {new Policy(Resource.Users, Method.Read)});
        
            authorized.Should().BeTrue();
        }
    }
    
    [Test]
    public void UnauthorizedPermissions()
    {
        var permissions = new[]
        {
            "none",
            "users-read"
        };

        foreach (var permission in permissions)
        {
            var authorized = PolicyAuthorizer.Authorize(permission, new[] {new Policy(Resource.Users, Method.Write)});
        
            authorized.Should().BeFalse();
        }
    }
}