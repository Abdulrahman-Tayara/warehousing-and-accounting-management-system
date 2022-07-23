using Application.Common.Security;
using NUnit.Framework;
using FluentAssertions;

namespace Application.UnitTests.Common.Security;

public class PolicyHelperTests
{
    [Test]
    public void PolicyToString()
    {
        var policy = new Policy(Resource.Users, Method.Read);

        var policyString = PolicyHelper.PolicyToString(policy);

        policyString.Should().BeEquivalentTo("users-read");
    }

    [Test]
    public void IsValidPolicy_Valid()
    {
        var isValid = PolicyHelper.IsValidPolicy("users-read");

        isValid.Should().BeTrue();
    }

    [Test]
    public void IsValidPolicy_Invalid()
    {
        var isValid = new[]
        {
            PolicyHelper.IsValidPolicy("users/read"),
            PolicyHelper.IsValidPolicy("read-users"),
            PolicyHelper.IsValidPolicy("user-read"),
        };

        foreach (var b in isValid)
        {
            b.Should().BeFalse();
        }
    }

    [Test]
    public void CreatePolicyFromString_Valid()
    {
        var policy = PolicyHelper.CreatePolicyFromString("users-read");

        policy.Resource.Should().Be(Resource.Users);
        policy.Method.Should().Be(Method.Read);
    }

    [Test]
    public void CreatePolicyFromString_Invalid()
    {
        Policy? policy = null;
        try
        {
            policy = PolicyHelper.CreatePolicyFromString("user-read");
            policy.Should().NotBeNull();
        }
        catch (Exception _)
        {
            policy.Should().BeNull();
        }
    }

    [Test]
    public void PoliciesToString()
    {
        var policies = new List<Policy>
        {
            new(Resource.Users, Method.Delete),
            new(Resource.Accounts, Method.Read)
        };

        var policiesString = PolicyHelper.PoliciesToString(policies);

        policiesString.Should().BeEquivalentTo("users-delete,accounts-read");
    }

    [Test]
    public void PoliciesToString_Single()
    {
        var policies = new List<Policy>
        {
            new(Resource.Users, Method.Delete)
        };

        var policiesString = PolicyHelper.PoliciesToString(policies);

        policiesString.Should().BeEquivalentTo("users-delete");
    }

    [Test]
    public void CreatePoliciesFromString()
    {
        var policiesString = "users-read,accounts-delete";

        var policies = PolicyHelper.CreatePoliciesFromString(policiesString);

        var enumerable = policies.ToList();
        
        enumerable.ToList().Count.Should().Be(2);
        
        foreach (var policy in enumerable)
        {
            policy.Should().NotBeNull();
        }
    }
    
    [Test]
    public void CreatePoliciesFromString_Single()
    {
        var policiesString = "users-read";

        var policies = PolicyHelper.CreatePoliciesFromString(policiesString);

        var enumerable = policies.ToList();
        
        enumerable.ToList().Count.Should().Be(1);

        enumerable[0].Resource.Should().Be(Resource.Users);
        enumerable[0].Method.Should().Be(Method.Read);
    }
}