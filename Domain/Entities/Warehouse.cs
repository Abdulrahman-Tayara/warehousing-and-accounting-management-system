namespace Domain.Entities;

public class Warehouse : BaseEntity<int>
{
    public string Name { get; set; }
    
    public string Location { get; set; }
}