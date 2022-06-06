namespace Domain.Entities;

public class Account : BaseEntity<int>
{
    public string Code { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string City { get; set; }

    public Account(int id, string code, string name, string phone, string city)
    {
        Id = id;
        Code = code;
        Name = name;
        Phone = phone;
        City = city;
    }
}