namespace Domain.Entities;

public class Warehouse : BaseEntity<int>
{
    public string Name { get; set; }
    
    public string Location { get; set; }
    
    public int CashDrawerAccountId { get; set; }

    public Warehouse(int id, string name, string location, int cashDrawerAccountId)
    {
        Id = id;
        Name = name;
        Location = location;
        CashDrawerAccountId = cashDrawerAccountId;
    }
}