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

    // An invoice is closed until products are added
    public InvoiceStatus Status { get; set; }

    public InvoiceType Type { get; set; }
    
    public InvoiceAccountType AccountType { get; set; }

    public IList<ProductMovement> Items { get; set; }

    public Invoice(int? accountId, int warehouseId, int? currencyId, string? note, DateTime createdAt, InvoiceType type, InvoiceAccountType accountType, IList<ProductMovement> items)
    {
        AccountId = accountId;
        WarehouseId = warehouseId;
        CurrencyId = currencyId;
        TotalPrice = 0;
        Note = note;
        CreatedAt = createdAt;
        Status = InvoiceStatus.Closed;
        Type = type;
        AccountType = accountType;
        Items = new List<ProductMovement>();
        items.ToList().ForEach(item => AddItem(item));
    }

    public void AddItem(ProductMovement item)
    {
        TotalPrice += item.TotalPrice;

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

        Status = InvoiceStatus.Closed;
    }

    public void Open()
    {
        if (Status == InvoiceStatus.Opened)
        {
            throw new InvoiceOpenedException();
        }

        Status = InvoiceStatus.Opened;
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

public enum InvoiceAccountType
{
    PurchasesSales, // Related to Purchases and Sales accounts
    ImportExport, // Related to Imports and Exports accounts
    Returns // Related to PurchasesReturns and SalesReturns account
}

public static class InvoiceAccountTypeExtensions
{
    public static bool DealsWithPurchasesSales(this InvoiceAccountType invoiceAccountType)
    {
        return invoiceAccountType switch
        {
            InvoiceAccountType.PurchasesSales => true,
            InvoiceAccountType.Returns => true,
            _ => false
        };
    }
    
    public static bool DealsWithReturns(this InvoiceAccountType invoiceAccountType)
    {
        return invoiceAccountType switch
        {
            InvoiceAccountType.Returns => true,
            _ => false
        };
    }
}