namespace Domain.Entities;

public class Conversion : BaseEntity<int>
{
    public int FromWarehouseId { get; set; }
    public Warehouse? FromWarehouse { get; set; }
    
    public int ToWarehouseId { get; set; }
    public Warehouse? ToWarehouse { get; set; }
    
    public int FromProductId { get; set; }
    public Product? FromProduct { get; set; }
    
    public int ToProductId { get; set; }
    public Product? ToProduct { get; set; }
    
    public int FromPlaceId { get; set; }
    public StoragePlace? FromPlace { get; set; }
    
    public int ToPlaceId { get; set; }
    public StoragePlace? ToPlace { get; set; }
    
    public int FromQuantity { get; set; }
    
    public int ToQuantity { get; set; }
    
    public string? Note { get; set; }
    
    public int ExportInvoiceId { get; set; }
    public Invoice? ExportInvoice { get; set; }
    
    public int ImportInvoiceId { get; set; }
    public Invoice? ImportInvoice { get; set; }
}