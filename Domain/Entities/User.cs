namespace Domain.Entities;

public class User
{
    public int Id { get; }
    
    public string UserName { get; set; }

    public string PasswordHash { get; set; }

}