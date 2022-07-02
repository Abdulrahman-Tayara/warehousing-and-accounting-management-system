namespace Application.Common.Security;

public static class PolicyHelper
{
    private const string Separator = "-";
    public const string PoliciesSeparator = ",";

    public static Policy CreatePolicyFromString(string policy)
    {
        if (!IsValidPolicy(policy))
            throw new InvalidDataException($"Invalid policy string {policy}");

        var splits = policy.Split(Separator);
        var resourceString = splits[0];
        var methodString = splits[1];

        var resource = ResourceHelper.FromString(resourceString);
        var method = MethodHelper.FromString(methodString);

        return new Policy((Resource) resource!, (Method) method!);
    }

    public static IEnumerable<Policy> CreatePoliciesFromString(string policies)
    {
        return policies.Split(PoliciesSeparator)
            .Select(CreatePolicyFromString);
    }

    public static bool IsValidPolicy(string policyString)
    {
        var policies = policyString.Split(PoliciesSeparator);

        return policies.All(policy =>
        {
            var splits = policy.Split(Separator);

            if (splits.Length != 2)
            {
                return false;
            }

            var resource = splits[0];
            var method = splits[1];

            return ResourceHelper.IsValidResource(resource) && MethodHelper.IsValidMethod(method);
        });
    }

    public static string PolicyToString(Policy policy)
    {
        return $"{policy.Resource.ToString()}{Separator}{policy.Method.ToString()}";
    }

    public static string PoliciesToString(IEnumerable<Policy> policies)
    {
        return string.Join(PoliciesSeparator, policies.Select(PolicyToString));
    }
}

internal static class MethodHelper
{
    public static bool IsValidMethod(string method)
    {
        var methods = Enum.GetValues<Method>()
            .ToList();

        return methods.Any(methodEnum => method.Equals(methodEnum.ToString(), StringComparison.OrdinalIgnoreCase));
    }

    public static Method? FromString(string method)
    {
        return Enum.GetValues<Method>()
            .FirstOrDefault(m => m.ToString().Equals(method, StringComparison.OrdinalIgnoreCase));
    }
}

internal static class ResourceHelper
{
    public static bool IsValidResource(string resource)
    {
        var resources = Enum.GetValues<Resource>()
            .ToList();

        return resources.Any(resourceEnum => resource.Equals(resourceEnum.ToString(), StringComparison.OrdinalIgnoreCase));
    }

    public static Resource? FromString(string resource)
    {
        return Enum.GetValues<Resource>()
            .FirstOrDefault(r => r.ToString().Equals(resource, StringComparison.OrdinalIgnoreCase));
    }
}