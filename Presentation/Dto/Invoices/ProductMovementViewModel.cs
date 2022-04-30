using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;
using wms.Dto.CurrencyAmounts;

namespace wms.Dto.Invoices;

public class ProductMovementViewModel : IViewModel, IMapFrom<ProductMovement>
{
    public int InvoiceId { get; set; }
    public Product? Product { get; set; }
    public int PlaceId { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public double TotalPrice { get; set; }
    public Currency Currency { get; set; }
    public IEnumerable<CurrencyAmountViewModel> CurrencyAmounts { get; set; }
    public string? Note { get; set; }
    public ProductMovementType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}