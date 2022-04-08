namespace Domain.Entities;

public class Currency : BaseEntity<int>
{
    public string Name { get; set; }
    
    public string Symbol { get; set; }
    
    public float Factor { get; set; }
}