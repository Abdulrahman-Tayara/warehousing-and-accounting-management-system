namespace Application.Common.Security;

public class Role
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public Permissions Permissions { get; set; }

    public Role(int id, string name, Permissions permissions)
    {
        Id = id;
        Name = name;
        Permissions = permissions;
    }

    public Role(string name, Permissions permissions)
    {
        Name = name;
        Permissions = permissions;
    }
}