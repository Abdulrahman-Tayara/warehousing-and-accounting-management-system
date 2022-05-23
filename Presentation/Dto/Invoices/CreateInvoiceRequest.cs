using System.ComponentModel.DataAnnotations;
using Application.Commands.Invoicing;
using Application.Commands.Invoicing.Dto;
using Application.Common.Mappings;
using Domain.Entities;

namespace wms.Dto.Invoices;

public class CreateInvoiceRequest : IMapFrom<CreateInvoiceCommand>
{
    [Required]
    public int AccountId { get; set; }
    [Required]
    public int WarehouseId { get; set; }
    [Required]
    public int CurrencyId { get; set; }
    public string? Note { get; set; }
    [Required]
    public InvoiceType Type { get; set; }
    [Required]
    [MinLength(1)]
    public IEnumerable<InvoiceItemDto> Items { get; set; }

    public bool IgnoreMinLevelWarnings { get; set; } = false;
}