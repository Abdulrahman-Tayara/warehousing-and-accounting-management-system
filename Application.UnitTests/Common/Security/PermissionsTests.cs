using Application.Common.Security;
using FluentAssertions;
using NUnit.Framework;

namespace Application.UnitTests.Common.Security;

public class PermissionsTests
{
    [Test]
    public void CreatePermissionFromString()
    {
        var permissionsString = "users-read,accounts-write";

        var permissions = Permissions.From(permissionsString);

        permissions.AllPermissions.Should().BeFalse();
        permissions.None.Should().BeFalse();
        permissions.Policies.Count.Should().Be(2);
    }
    
    [Test]
    public void CreateAllPermissionFromString()
    {
        var permissionsString = "all";

        var permissions = Permissions.From(permissionsString);

        permissions.AllPermissions.Should().BeTrue();
        permissions.None.Should().BeFalse();
        permissions.Policies.Count.Should().Be(0);
    }
    
    [Test]
    public void CreateNonePermissionFromString()
    {
        var permissionsString = "none";

        var permissions = Permissions.From(permissionsString);

        permissions.AllPermissions.Should().BeFalse();
        permissions.None.Should().BeTrue();
        permissions.Policies.Count.Should().Be(0);
    }

    [Test]
    public void PermissionsToString()
    {
        var permissions = new Permissions();
        permissions.AddPolicy(new Policy(Resource.Users, Method.Read));
        permissions.AddPolicy(new Policy(Resource.Accounts, Method.Read));

        var permissionsString = permissions.ToString();

        permissionsString.Should().BeEquivalentTo("users-read,accounts-read");
    }
    
    [Test]
    public void PermissionsToString_AllIgnoresPolicies()
    {
        var permissions = new Permissions
        {
            AllPermissions = true
        };
        permissions.AddPolicy(new Policy(Resource.Users, Method.Read));

        var permissionsString = permissions.ToString();

        permissionsString.Should().BeEquivalentTo("all");
    }
}