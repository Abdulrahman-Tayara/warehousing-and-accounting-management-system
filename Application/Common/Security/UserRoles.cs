using Domain.Entities;

namespace Application.Common.Security;

public class UserRoles
{
    public User User { get; set; }
    public IList<Role> Roles { get; set; }

    public UserRoles(User user, IList<Role> roles)
    {
        User = user;
        Roles = roles;
    }

    public void UpdateRoles(IEnumerable<Role> roles)
    {
        Roles.Clear();
        foreach (var role in roles)
        {
            if (!Roles.Contains(role))
                Roles.Add(role);
        }
    }

    public IList<string> RoleStrings => Roles.Select(r => r.Name).ToList();
}