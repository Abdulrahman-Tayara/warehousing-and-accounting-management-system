namespace Application.Common.Security;

public class Permissions
{
    private const string AllPermissionKey = "all";
    private const string NonePermissionKey = "none";

    public bool AllPermissions { get; set; }
    public bool None { get; set; }

    public IList<Policy> Policies { get; set; } = new List<Policy>();

    public void AddPolicy(Policy policy)
    {
        if (Policies.Contains(policy))
            return;

        Policies.Add(policy);
    }

    public void Merge(Permissions permissions)
    {
        if (permissions.AllPermissions)
            AllPermissions = true;
        else if (permissions.None)
            None = true;
        else
        {
            foreach (var permissionsPolicy in permissions.Policies)
            {
                AddPolicy(permissionsPolicy);
            }
        }
    }

    public static Permissions From(string? permission)
    {
        var permissions = new Permissions();

        if (permission == null)
            return permissions;

        foreach (var policy in permission.Split(PolicyHelper.PoliciesSeparator))
        {
            if (policy.Equals(AllPermissionKey, StringComparison.OrdinalIgnoreCase))
            {
                permissions.AllPermissions = true;
                break;
            }

            if (policy.Equals(NonePermissionKey, StringComparison.OrdinalIgnoreCase))
            {
                permissions.None = true;
                break;
            }

            permissions.AddPolicy(PolicyHelper.CreatePolicyFromString(policy));
        }

        return permissions;
    }

    public override string ToString()
    {
        if (AllPermissions)
        {
            return AllPermissionKey;
        }

        if (None)
        {
            return NonePermissionKey;
        }

        return string.Join(PolicyHelper.PoliciesSeparator, PolicyHelper.PoliciesToString(Policies));
    }
}