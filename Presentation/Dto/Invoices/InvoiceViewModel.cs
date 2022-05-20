using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Accounts;
using wms.Dto.Common;
using wms.Dto.Currencies;
using wms.Dto.Warehouses;

namespace wms.Dto.Invoices;

public class InvoiceViewModel : IViewModel, IMapFrom<Invoice>
{
    public int Id { get; set; }
    public AccountViewModel Account { get; set; } = default!;
    public WarehouseViewModel Warehouse { get; set; } = default!;
    public CurrencyViewModel? Currency { get; set; }
    public double TotalPrice { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
    public InvoiceStatus Status { get; set; }
    public InvoiceType Type { get; set; }
}