namespace Domain.Entities;

public class User : BaseEntity<int>
{
    public string UserName { get; set; }

    public string PasswordHash { get; set; }

}