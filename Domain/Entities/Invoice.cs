using Domain.Exceptions;

namespace Domain.Entities;

public class Invoice : BaseEntity<int>
{
    public int? AccountId { get; set; }
    public Account? Account { get; set; }

    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }

    public int? CurrencyId { get; set; }
    public Currency? Currency { get; set; }

    public double TotalPrice { get; set; }
    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }

    public InvoiceStatus Status { get; set; }

    public InvoiceType Type { get; set; }

    public IList<ProductMovement> Items { get; set; } = new List<ProductMovement>();

    public void AddItem(ProductMovement item)
    {
        TotalPrice += item.TotalPrice;
        
        if (IsProductExists(item.ProductId))
        {
            var existsItem = Items.First(i => i.ProductId == item.ProductId);
            existsItem.IncreaseQuantity(item.Quantity);
        }
        else
        {
            Items.Add(item);
        }
    }

    public bool IsProductExists(int productId) => Items.Any(i => i.ProductId == productId);

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

        Status = InvoiceStatus.Closed;
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