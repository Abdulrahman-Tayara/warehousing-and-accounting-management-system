namespace Domain.Entities;

public class CurrencyAmount : BaseEntity<int>
{
    public int ObjectId { get; set; }
    
    public CurrencyAmountKey Key { get; set; }
    
    public double Amount { get; set; }
    
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
}

public enum CurrencyAmountKey
{
    Movement,
    Payment
}