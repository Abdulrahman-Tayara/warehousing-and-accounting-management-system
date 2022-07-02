namespace Application.Common.Security;

public class Permissions
{
    private const string AllPermissionKey = "all";
    private const string NonePermissionKey = "none";

    public bool AllPermissions { get; set; }
    public bool None { get; set; }

    public readonly IList<Policy> Policies = new List<Policy>();

    public void AddPolicy(Policy policy)
    {
        if (Policies.Contains(policy))
            return;

        Policies.Add(policy);
    }

    public void Merge(Permissions permissions)
    {
        AllPermissions = AllPermissions || permissions.AllPermissions;
        None = None || permissions.None;
        foreach (var permissionsPolicy in permissions.Policies)
        {
            AddPolicy(permissionsPolicy);
        }
    }

    public static Permissions From(string permission)
    {
        var permissions = new Permissions();

        foreach (var policy in permission.Split(PolicyHelper.PoliciesSeparator))
        {
            if (policy.Equals(AllPermissionKey, StringComparison.OrdinalIgnoreCase))
                permissions.AllPermissions = true;
            else if (policy.Equals(NonePermissionKey, StringComparison.OrdinalIgnoreCase))
                permissions.None = true;
            else
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
        else if (None)
        {
            return NonePermissionKey;
        }
        
        return string.Join(PolicyHelper.PoliciesSeparator, PolicyHelper.PoliciesToString(Policies));
    }
}