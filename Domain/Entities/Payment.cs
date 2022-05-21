namespace Domain.Entities;

public class Payment : BaseEntity<int>
{
    public int InvoiceId { get; set; }

    public string? Note { get; set; }

    public PaymentType PaymentType { get; set; }

    public PaymentIoType PaymentIoType { get; set; }

    public double Amount { get; set; }

    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }

    public IEnumerable<CurrencyAmount>? CurrencyAmounts { get; set; }

    public DateTime CreatedAt { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Payment payment && payment.Id == Id;
    }
    
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

public enum PaymentType
{
    Normal,
    Discount
}

public enum PaymentIoType
{
    In,
    Out
}