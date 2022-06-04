using Domain.Events;
using Domain.Exceptions;

namespace Domain.Entities;

public class Invoice : BaseEntity<int>
{
    public int? AccountId { get; }
    public Account? Account { get; }

    public int WarehouseId { get; }
    public Warehouse Warehouse { get; }

    public int? CurrencyId { get; }
    public Currency? Currency { get; }

    private double _totalPrice { get; set; }
    public double TotalPrice => _totalPrice;
    
    public string? Note { get; }

    public DateTime CreatedAt { get; }

    
    private InvoiceStatus _status { get; set; }
    public InvoiceStatus Status => _status;

    public InvoiceType Type { get; }

    public IList<ProductMovement> Items { get; }

    public IList<DomainEvent> Events { get; set; }

    public Invoice(int? accountId, int warehouseId, int? currencyId, string? note, DateTime createdAt, InvoiceType type, IList<ProductMovement> items)
    {
        AccountId = accountId;
        WarehouseId = warehouseId;
        CurrencyId = currencyId;
        _totalPrice = 0;
        Note = note;
        CreatedAt = createdAt;
        _status = InvoiceStatus.Closed;
        Type = type;
        Items = new List<ProductMovement>();
        items.ToList().ForEach(item => AddItem(item));
    }

    public void AddItem(ProductMovement item)
    {
        _totalPrice += item.TotalPrice;

        if (AddedProductOpensInvoice())
        {
            Open();
        }
        
        Items.Add(item);
    }

    private bool AddedProductOpensInvoice() => TotalPrice != 0 && Status == InvoiceStatus.Closed;

    public bool ProductExists(int productId) => Items.Any(i => i.ProductId == productId);

    public bool IsClosed()
    {
        return Status == InvoiceStatus.Closed;
    }

    public void Close()
    {
        if (Status == InvoiceStatus.Closed)
        {
            throw new InvoiceClosedException();
        }

        _status = InvoiceStatus.Closed;
    }

    public void Open()
    {
        if (Status == InvoiceStatus.Opened)
        {
            throw new InvoiceOpenedException();
        }

        _status = InvoiceStatus.Opened;
    }
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