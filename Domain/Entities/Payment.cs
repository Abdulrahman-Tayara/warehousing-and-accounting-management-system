namespace Domain.Entities;

public class Payment : BaseEntity<int>
{
    public int InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }

    public string? Note { get; set; }

    public PaymentType PaymentType { get; set; }

    public PaymentIoType PaymentIoType { get; set; }
    
    public double Amount { get; set; }
    
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    
    public IEnumerable<CurrencyAmount> CurrencyAmounts { get; set; }

    public DateTime CreatedAt { get; set; }
}

public enum PaymentType
{
    Normal,
}

public enum PaymentIoType
{
    In,
    Out
}