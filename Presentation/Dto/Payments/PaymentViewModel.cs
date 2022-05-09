using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;

namespace wms.Dto.Payments;

public class PaymentViewModel : IMapFrom<Payment>, IViewModel
{
    public int InvoiceId { get; set; }

    public string? Note { get; set; }

    public PaymentType PaymentType { get; set; }

    public PaymentIoType PaymentIoType { get; set; }
    
    public double Amount { get; set; }
    
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    
    public IEnumerable<CurrencyAmount> CurrencyAmounts { get; set; }

    public DateTime CreatedAt { get; set; }
}