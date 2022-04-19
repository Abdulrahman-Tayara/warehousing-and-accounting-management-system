namespace Domain.Entities;

public class Invoice : BaseEntity<int>
{
    public int AccountId { get; set; }
    public Account Account { get; set; }
    
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    
    public int TotalPrice { get; set; }

    public IEnumerable<ProductMovement> Items { get; set; }
    
    public string Note { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public InvoiceStatus Status { get; set; }

    public InvoiceType Type { get; set; }
}

public enum InvoiceStatus
{
    
}

public enum InvoiceType
{
    
}