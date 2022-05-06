namespace Domain.Entities;

public class Invoice : BaseEntity<int>
{
    public int AccountId { get; set; }
    public Account Account { get; set; }
    
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    
    public int? CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    
    public double TotalPrice { get; set; }
    public string? Note { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public InvoiceStatus Status { get; set; }

    public InvoiceType Type { get; set; }
}

public enum InvoiceStatus
{
    Opened,
    Closed
}

public enum InvoiceType
{
    In,
    Out
}