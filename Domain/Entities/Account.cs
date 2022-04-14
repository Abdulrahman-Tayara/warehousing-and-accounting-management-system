namespace Domain.Entities;

public class Account : BaseEntity<int>
{
    public string Code { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string City { get; set; }
}