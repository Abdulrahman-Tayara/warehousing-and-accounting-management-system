using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;

namespace wms.Dto.Invoices;

public class InvoiceViewModel : IViewModel, IMapFrom<Invoice>
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int WarehouseId { get; set; }
    public int? CurrencyId { get; set; }
    public double TotalPrice { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
    public InvoiceStatus Status { get; set; }
    public InvoiceType Type { get; set; }
}