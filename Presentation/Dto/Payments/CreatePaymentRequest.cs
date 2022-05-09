using System.ComponentModel.DataAnnotations;
using Application.Commands.Payments;
using Application.Common.Dtos;
using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.CurrencyAmounts;

namespace wms.Dto.Payments;

public class CreatePaymentRequest : IMapFrom<CreatePaymentCommand>
{
    [Required]
    public int InvoiceId { get; set; }

    public string? Note { get; set; }

    [Required]
    public double Amount { get; set; }

    [Required]
    public int CurrencyId { get; set; }
    
    public IEnumerable<CurrencyAmountDto>? CurrencyAmounts { get; set; }
}